using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmoPickup : AmmoPickup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        ammoType = ammoTypes.Shotgun;
        base.OnTriggerEnter2D(other);
    }
}
