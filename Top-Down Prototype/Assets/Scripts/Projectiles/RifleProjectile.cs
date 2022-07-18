using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitObject = collision.gameObject;
        if (hitObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DealDamage(damage, gameObject);
        }
    }
}
