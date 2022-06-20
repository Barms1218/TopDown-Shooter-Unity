using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    public override void Fire(Vector2 direction)
    {
        currentAmmo -= data.AmmoPerShot;
        hud.CurrentAmmo = currentAmmo;

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
        reloading = true;
        yield return new WaitForSeconds(data.ReloadSpeed);

        currentAmmo = data.MaxAmmo;
        hud.DisplayAmmo(currentAmmo, data.MaxAmmo);        
        reloading = false;
        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
