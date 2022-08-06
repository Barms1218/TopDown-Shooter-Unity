using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class WeaponSwap : MonoBehaviour
{
    [SerializeField] PlayerWeaponHandler weaponHandler;
    [SerializeField] GameObject gun;
    private List<Weapon> weaponList = new List<Weapon>();
    private Weapon currentWeapon;

    private void Awake()
    {
        currentWeapon = gun.GetComponent<Weapon>();
        weaponHandler.CurrentWeapon = currentWeapon;
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
        weaponHandler.TriggerDelay = currentWeapon.TimeBetweenShots;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo,
            currentWeapon.MaxAmmo);
    }

    //public void GetNewWeapon(Weapon newGun)
    //{
    //    AudioManager.Play(AudioClipName.GetGun);
    //    gun.SetActive(false);
    //    weaponList.Add(newGun);
    //    newGun.transform.SetParent(transform, false);
    //    newGun.transform.position = gun.transform.position;
    //    gun = newGun.gameObject;

    //    currentWeapon = newGun;
    //    //timeBetweenShots = new WaitForSeconds(currentWeapon.TimeBetweenShots);
    //    currentWeapon.Collider.enabled = false;
    //    UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    //}

    public void GetNewWeapon(Weapon newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        //gun.SetActive(false);
        weaponList.Add(newGun);
        newGun.transform.SetParent(transform, false);
        newGun.transform.position = newGun.gameObject.transform.position;
        //gun = newGun.gameObject;

        weaponHandler.CurrentWeapon = newGun;
        weaponHandler.TriggerDelay = newGun.TimeBetweenShots;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(newGun.CurrentAmmo, newGun.MaxAmmo);
    }

    public void AddAmmoToWeapon(int amountToAdd, Weapon weapon)
    {
        if (weaponList.Contains(weapon))
        {
            weapon.MaxAmmo += amountToAdd;
        }
    }

    private void OnEnable()
    {
        Pickup.getWeapon += GetNewWeapon;
    }

    private void OnDisable()
    {
        Pickup.getWeapon -= GetNewWeapon;
    }
}
