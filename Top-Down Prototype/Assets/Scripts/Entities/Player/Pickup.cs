using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class Pickup : MonoBehaviour
{
    [SerializeField]
    PlayerWeaponHandler weaponHandler;
    public static UnityAction<Weapon> getWeapon;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupObject = collision.gameObject;
        if (pickupObject.TryGetComponent(out Weapon weapon))
        {
            //GetNewWeapon(weapon);
            pickupObject.GetComponent<Collider2D>().enabled = false;
            getWeapon?.Invoke(weapon);
        }
        //else if (pickupObject.TryGetComponent(out AmmoPickup ammoPickup))
        //{
        //    weaponHandler.AddAmmoToWeapon(ammoPickup.AmountToAdd, ammoPickup.WeaponType);
        //}
    }

    //public void GetNewWeapon(Weapon newGun)
    //{
    //    AudioManager.Play(AudioClipName.GetGun);
    //    //gun.SetActive(false);
    //    weaponList.Add(newGun);
    //    newGun.transform.SetParent(transform, false);
    //    newGun.transform.position = newGun.gameObject.transform.position;
    //    //gun = newGun.gameObject;

    //    weaponHandler.CurrentWeapon = newGun;
    //    //timeBetweenShots = new WaitForSeconds(currentWeapon.TimeBetweenShots);
    //    UpdateAmmoUI.Instance.UpdateWeaponAmmo(newGun.CurrentAmmo, newGun.MaxAmmo);
    //}
}
