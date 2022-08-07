using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerShoot))]
public class PlayerHolster : MonoBehaviour
{
    PlayerShoot shooter;
    GameObject gun;
    private List<Gun> weaponList = new();
    private Gun currentWeapon;
    [SerializeField] Transform weaponParent;


    public List<Gun> WeaponList => weaponList;
    public Gun CurrentWeapon { set => currentWeapon = value; }
    public GameObject Gun
    {
        get => gun;
        set => gun = value;
    }

    private void Awake()
    {
        shooter = GetComponent<PlayerShoot>();
        currentWeapon = GetComponentInChildren<Gun>();

        gun = currentWeapon.gameObject;
        //gun.transform.SetParent(transform, false);
        weaponList.Add(currentWeapon);
        shooter.CurrentWeapon = currentWeapon;
    }

    public void TryEquipWeaponOne()
    {
        try
        {
            ChangeWeapon(0);

        }
        catch (System.Exception e)
        {
            gun.SetActive(true);
            Debug.Log(e);
        }
    }
    public void TryEquipWeaponTwo()
    {
        try
        {
            ChangeWeapon(1);
        }
        catch (System.Exception e)
        {
            gun.SetActive(true);
            Debug.Log(e);
        }
    }
    public void TryEquipWeaponThree()
    {
        try
        {
            ChangeWeapon(2);
        }
        catch (System.Exception e)
        {
            gun.SetActive(true);
            Debug.Log(e);
        }
    }

    void ChangeWeapon(int weaponIndex)
    {
        gun.SetActive(false);
        gun = weaponList[weaponIndex].gameObject;
        gun.SetActive(true);
        currentWeapon = weaponList[weaponIndex];
        shooter.CurrentWeapon = currentWeapon;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo,
            currentWeapon.MaxAmmo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupObject = collision.gameObject;
        if (pickupObject.TryGetComponent(out Gun weapon))
        {
            GetNewWeapon(weapon);
        }
    }

    public void GetNewWeapon(Gun newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gun.SetActive(false);
        weaponList.Add(newGun);
        newGun.transform.SetParent(transform, false);
        newGun.transform.position = gun.transform.position;
        gun = newGun.gameObject;

        shooter.CurrentWeapon = newGun;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(newGun.CurrentAmmo, newGun.MaxAmmo);
    }

    public void AddAmmoToWeapon(int amountToAdd, Gun weapon)
    {
        if (weaponList.Contains(weapon))
        {
            weapon.MaxAmmo += amountToAdd;
        }
    }
}
