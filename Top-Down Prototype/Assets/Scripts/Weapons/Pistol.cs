using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    
    protected override void ShootWeapon()
    {
        var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
            Quaternion.identity);

        var bulletScript = projectile.GetComponent<Projectile>();

        bulletScript.MoveToTarget(direction);
        AudioManager.Play(AudioClipName.PistolShot);
    }

    protected override void SpecialAttack()
    {
        while(true)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
        }
    }

    protected override void Reload()
    {
        base.Reload();
        AudioManager.Play(AudioClipName.PistolStartReload);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator StartReload()
    {
        yield return base.StartReload();
        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
