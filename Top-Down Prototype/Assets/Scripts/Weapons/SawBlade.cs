using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Weapon
{
    float specialAttackDelay = 2f;
    float timeToSpecialAttack;

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
        timeToSpecialAttack = specialAttackDelay;

        if (currentAmmo == maxAmmo)
        {
            timeToSpecialAttack -= Time.deltaTime;

            if (specialAttackDelay <= 0)
            {
                timeToSpecialAttack = specialAttackDelay;
                ShootWeapon();
            }
        }
    }
}
