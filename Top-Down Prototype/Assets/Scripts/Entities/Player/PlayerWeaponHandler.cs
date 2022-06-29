using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(InputController))]
public class PlayerWeaponHandler : WeaponHandler
{
    #region Fields

    [SerializeField] private List<GameObject> weaponList = new List<GameObject>();
    public static UnityAction<int, int> ReduceAmmo;
    public static UnityAction<int, int> SetAmmoCount;

    #endregion

    #region Properties

    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }
    public List<GameObject> WeaponList => weaponList;
    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    #endregion

    private void Awake()
    {
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();
        WeaponSwap.OnWeaponSwap += ChangeWeapon;
        Weapon.OnPickUp += GetNewWeapon;
    }

    private void Start()
    {
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        EntityInput.OnReload += Reload;
        EntityInput.OnSpecialAttack += SpecialAttack;
        EntityInput.OnFire += Fire;        
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;        
        currentWeapon.Aim(angle, GameObject.FindGameObjectWithTag("Cursor").transform);
    }

    #region Event Methods

    protected override void Fire()
    {
        if (currentWeapon.CurrentAmmo > 0 && CanFire)
        {
            currentWeapon.Fire(aimDirection);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;            
        }
        ReduceAmmo?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    #endregion

    private void ChangeWeapon(int weaponIndex)
    {
        if (weaponList[weaponIndex] != null)
        {
            gun.SetActive(false);
            gun = weaponList[weaponIndex];
            weaponList[weaponIndex].SetActive(true);
            currentWeapon = gun.GetComponent<Weapon>();

            SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        }
       
    }

    private void GetNewWeapon(GameObject newGun)
    {
        Gun.SetActive(false);        
        WeaponList.Add(newGun);
        newGun.transform.SetParent(transform);
        newGun.transform.position = Gun.transform.position;
        Gun = newGun;      
        Gun.GetComponent<Weapon>().enabled = true;
        
        // Flip the weapon to proper scale to match player's
        if (newGun.transform.position.x < transform.position.x)
        {
            Vector3 newScale = Gun.transform.localScale;
            newScale.x *= -1;
            Gun.transform.localScale = newScale;
        }          
        CurrentWeapon = newGun.GetComponent<Weapon>();
        Gun.SetActive(true);
        Gun.GetComponent<Collider2D>().enabled = false;
        PlayerWeaponHandler.SetAmmoCount?.Invoke(CurrentWeapon.CurrentAmmo,
        CurrentWeapon.CurrentAmmo);
    }
}
