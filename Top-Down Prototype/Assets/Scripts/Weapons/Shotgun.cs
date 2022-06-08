using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    int numProjectiles = 5;
    [SerializeField]
    float pelletSpread = 0.5f;
    protected override IEnumerator ContinuousFire()
    {
        while (true)
        {
            if (currentAmmo >= 1 && !reloading)
            {
                ShootWeapon();
                hud.ReduceAmmoCount(ammoPerShot);
                currentAmmo -= ammoPerShot;
                reloadSpeed = maxAmmo - currentAmmo;
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }
    protected override void ShootWeapon()
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            Quaternion pelletRotation = Quaternion.Euler(0f, 0f, direction.z);
            var pellet = Instantiate(projectilePrefab, muzzleTransform.position, 
                pelletRotation);

            var pelletScript = pellet.GetComponent<PelletProjectile>();
            direction.y -= Random.Range(-pelletSpread, pelletSpread);
            pelletScript.MoveToTarget(direction);
        }

    }

    protected override void SpecialAttack()
    {

    }
}
