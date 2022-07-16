using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(InputController))]
public class PlayerWeaponHandler : WeaponHandler
{
    #region Fields

    [SerializeField] private Dictionary<string, GameObject> weaponDictionary = 
    new Dictionary<string, GameObject>();
    public static UnityAction<int, int> ReduceAmmo;
    public static UnityAction<int, int> SetAmmoCount;

    #endregion

    #region Properties

    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }
    public Dictionary<string, GameObject> WeaponDictionary => weaponDictionary;
    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    #endregion

    private void Awake()
    {
        weaponDictionary.Add(gun.name, gun);
        currentWeapon = gun.GetComponent<Weapon>();
        WeaponSwap.OnWeaponSwap += ChangeWeapon;
        Weapon.OnPickUp += GetNewWeapon;
        AmmoPickup.OnTrigger += AddAmmoToWeapon;
    }

    private void Start()
    {
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        EntityInput.OnReload += Reload;
        EntityInput.OnSpecialAttack += SpecialAttack;
        EntityInput.OnFire += Fire;
    }

    #region Event Methods

    public override void Fire()
    {
        if (currentWeapon.CurrentAmmo > 0 && CanFire)
        {
            currentWeapon.Fire(aimDirection);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
                        
        }
        else if (currentWeapon.CurrentAmmo == 0 && CanFire)
        {
            AudioManager.Play(AudioClipName.NoAmmo);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;  
        }
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    public void Aim(InputAction.CallbackContext value)
    {
        var _direction = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion gunRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gun.transform.rotation = gunRotation;

    }

    #endregion

    private void ChangeWeapon(string weaponkey)
    {
        if (weaponDictionary.ContainsKey(weaponkey))
        {
            gun.SetActive(false);
            gun = weaponDictionary[weaponkey];
            weaponDictionary[weaponkey].SetActive(true);
            currentWeapon = gun.GetComponent<Weapon>();

            SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        }
    }

    private void GetNewWeapon(GameObject newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gun.SetActive(false);        
        weaponDictionary.Add(newGun.name, newGun);
        newGun.transform.SetParent(transform);
        newGun.transform.position = gun.transform.position;
        gun = newGun;      
        gun.GetComponent<Weapon>().enabled = true;
        
        // Flip the weapon to proper scale to match player's
        if (newGun.transform.position.x < transform.position.x)
        {
            Vector3 newScale = gun.transform.localScale;
            newScale.x *= -1;
            gun.transform.localScale = newScale;
        }          
        currentWeapon = newGun.GetComponent<Weapon>();
        gun.SetActive(true);
        gun.GetComponent<Collider2D>().enabled = false;
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    private void AddAmmoToWeapon(int amountToAdd, string weaponName)
    {
        if (weaponDictionary.ContainsKey(weaponName))
        {
            weaponDictionary[weaponName].GetComponent<Weapon>().MaxAmmo += amountToAdd;
            SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        }
    }
}
