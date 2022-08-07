using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerShoot))]
public class PlayerHolster : MonoBehaviour
{
    PlayerShoot shooter;
    [SerializeField] GameObject gun;
    private List<Weapon> weaponList = new List<Weapon>();
    private Weapon currentWeapon;

    public List<Weapon> WeaponList => weaponList;
    public Weapon CurrentWeapon { set => currentWeapon = value; }
    public GameObject Gun
    {
        get => gun;
        set => gun = value;
    }

    private void Awake()
    {
        shooter = GetComponent<PlayerShoot>();
        currentWeapon = GetComponentInChildren<Weapon>();
        currentWeapon.transform.SetParent(transform, false);
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
        shooter.TriggerDelay = currentWeapon.TimeBetweenShots;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo,
            currentWeapon.MaxAmmo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupObject = collision.gameObject;
        if (pickupObject.TryGetComponent(out Weapon weapon))
        {
            GetNewWeapon(weapon);
        }
    }

    public void GetNewWeapon(Weapon newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gun.SetActive(false);
        weaponList.Add(newGun);
        newGun.transform.SetParent(transform, false);
        newGun.transform.position = gun.transform.position;
        gun = newGun.gameObject;

        shooter.CurrentWeapon = newGun;
        shooter.TriggerDelay = newGun.TimeBetweenShots;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(newGun.CurrentAmmo, newGun.MaxAmmo);
    }

    public void AddAmmoToWeapon(int amountToAdd, Weapon weapon)
    {
        if (weaponList.Contains(weapon))
        {
            weapon.MaxAmmo += amountToAdd;
        }
    }
}
