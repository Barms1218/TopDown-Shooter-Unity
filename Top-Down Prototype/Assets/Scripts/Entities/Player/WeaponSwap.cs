using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class WeaponSwap : MonoBehaviour
{
    PlayerWeaponHandler weaponHandler;

    private void Awake() => weaponHandler = GetComponent<PlayerWeaponHandler>();

    public void TryEquipWeaponOne(InputAction.CallbackContext context)
    {
        if (context.started)
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
    }
    public void TryEquipWeaponTwo(InputAction.CallbackContext context)
    {
        if (context.started)
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
    }
    public void TryEquipWeaponThree(InputAction.CallbackContext context)
    { 
        if (context.started)
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
    }
    void ChangeWeapon(int weaponIndex)
    {
        weaponHandler.Gun.SetActive(false);
        weaponHandler.Gun = weaponHandler.PlayerWeapons[weaponIndex];
        weaponHandler.Gun.SetActive(true);
        weaponHandler.CurrentWeapon = weaponHandler.Gun.GetComponent<Weapon>();
        weaponHandler.TriggerDelay = new WaitForSeconds(
            weaponHandler.CurrentWeapon.TimeBetweenShots);
        PlayerWeaponHandler.SetAmmoCount?.Invoke(
            weaponHandler.CurrentWeapon.CurrentAmmo, weaponHandler.CurrentWeapon.MaxAmmo);
    }

}
