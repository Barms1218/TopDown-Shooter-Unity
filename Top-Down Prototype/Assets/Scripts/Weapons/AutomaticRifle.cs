using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Weapon
{

    protected override void Fire()
    {
        base.Fire();
    }

    protected override IEnumerator ContinuousFire()
    {
        while (true)
        {
            if (currentAmmo >= 1 && !reloading)
            {
                ShootWeapon();
                hud.ReduceAmmoCount(ammoPerShot);
                currentAmmo -= ammoPerShot;
                AudioManager.Play(AudioClipName.AR_Fire);
                yield return new WaitForSeconds(timeBetweenShots);
            }

        }
    }
    /// <summary>
    /// 
    /// </summary>
    protected override void ShootWeapon()
    {
        var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
            Quaternion.identity);

        var bulletScript = projectile.GetComponent<Projectile>();

        bulletScript.MoveToTarget(direction);
    }

    /// <summary>
    /// When implemented, launch a grenade 
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }
}
