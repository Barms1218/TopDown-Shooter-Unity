using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Weapon
{
    float specialAttackDelay = 2f;

    protected override void Fire()
    {
        if (currentAmmo > 0 && !reloading)
        {
            Debug.Log("Shot Weapon");
            ShootWeapon();
            currentAmmo--;
        }
    }
    protected override void ShootWeapon()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var projectileScript = projectile.GetComponent<ProjectileScript>();

        projectileScript?.MoveToTarget(Vector2.right);

    }

    protected override void SpecialAttack()
    {
        if (currentAmmo == maxAmmo)
        {
            specialAttackDelay -= Time.deltaTime;

            if (specialAttackDelay <= 0)
            {
                specialAttackDelay = 2f;
                ShootWeapon();
            }
        }
    }
}
