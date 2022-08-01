using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class WeaponSwap : MonoBehaviour
{
    [SerializeField] PlayerWeaponHandler weaponHandler;
    [SerializeField] WeaponSelectorUI weaponSelector;

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
        weaponHandler.Gun = weaponHandler.PlayerWeapons[weaponIndex];
        weaponHandler.Gun.SetActive(true);



        weaponHandler.CurrentWeapon = weaponHandler.Gun.GetComponent<Weapon>();
        weaponHandler.TriggerDelay = new WaitForSeconds(
            weaponHandler.CurrentWeapon.TimeBetweenShots);
        HUD.Instance.UpdateWeaponAmmo(weaponHandler.CurrentWeapon.CurrentAmmo,
            weaponHandler.CurrentWeapon.MaxAmmo);
        weaponSelector.ShowImage(weaponHandler.Gun);
    }

}
