using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    PlayerWeaponHandler weaponHandler;

    private void Awake()
    {
        weaponHandler = GetComponent<PlayerWeaponHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupObject = collision.gameObject;
        if (pickupObject.TryGetComponent(out Weapon weapon))
        {
            weaponHandler.GetNewWeapon(pickupObject);
        }
        else if (pickupObject.TryGetComponent(out AmmoPickup ammoPickup))
        {
            weaponHandler.AddAmmoToWeapon(ammoPickup.Amount, ammoPickup.GunName);
        }
    }
}
