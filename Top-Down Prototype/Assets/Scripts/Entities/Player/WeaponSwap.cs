using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class WeaponSwap : MonoBehaviour
{
    [SerializeField] PlayerWeaponHandler weaponHandler;

    public void TryEquipWeaponOne()
    {
        try
        {
            ChangeWeapon(0);

        }
        catch (System.Exception e)
        {
            weaponHandler.Gun.SetActive(true);
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
            weaponHandler.Gun.SetActive(true);
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
            weaponHandler.Gun.SetActive(true);
            Debug.Log(e);
        }
    }

    void ChangeWeapon(int weaponIndex)
    {
        weaponHandler.Gun.SetActive(false);
        weaponHandler.Gun = weaponHandler.PlayerWeapons[weaponIndex].gameObject;
        weaponHandler.Gun.SetActive(true);
        weaponHandler.CurrentWeapon = weaponHandler.PlayerWeapons[weaponIndex];
        weaponHandler.TriggerDelay = new WaitForSeconds(
            weaponHandler.CurrentWeapon.TimeBetweenShots);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(weaponHandler.CurrentWeapon.CurrentAmmo,
            weaponHandler.CurrentWeapon.MaxAmmo);
    }

}
