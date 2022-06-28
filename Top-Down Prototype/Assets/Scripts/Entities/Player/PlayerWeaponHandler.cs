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

    private void GetNewWeapon()
    {

    }
}
