using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon, IFlippable
{

    protected override void Fire()
    {
        base.Fire();
        
         if (currentAmmo <= 0)
        {
            AudioManager.Play(AudioClipName.PistolEmpty);
        }
    }
    
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
        StartCoroutine(ReloadSound());
        AudioManager.Play(AudioClipName.PistolStartReload);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadSound()
    {
        yield return new WaitForSeconds(reloadSpeed);

        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
