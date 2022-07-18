using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmoPickup : AmmoPickup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        ammoType = ammoTypes.Rifle;
        base.OnTriggerEnter2D(other);
    }
}
