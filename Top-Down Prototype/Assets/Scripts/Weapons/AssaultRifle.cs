using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon, IFlippable
{

    /// <summary>
    /// 
    /// </summary>
    protected override void ShootWeapon()
    {
        var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
            Quaternion.identity);

        var bulletScript = projectile.GetComponent<Projectile>();

        bulletScript.MoveToTarget(direction);
        AudioManager.Play(AudioClipName.AR_Fire);
    }

    /// <summary>
    /// When implemented, launch a grenade 
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    protected override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator StartReload()
    {
        yield return base.StartReload();
        AudioManager.Play(AudioClipName.AR_Finish_Reload);
    }
}
