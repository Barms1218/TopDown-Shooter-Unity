using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Weapon
{

    protected override void ShootWeapon()
    {
        if (Physics2D.Raycast(transform.position, transform.right, maxRange))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, maxRange);

            if (hit.collider.GetComponent<Entity>() != null)
            {
                hit.collider.GetComponent<Entity>().TakeDamage(weaponDamage);
                Debug.Log("Enemy took " + weaponDamage + " damage");
            }
        }
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        var projectileScript = projectile.GetComponent<ProjectileScript>();

        projectileScript?.MoveToTarget(Vector2.right);

    }

    protected override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }
}
