using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    protected override void Fire()
    {
        if (currentAmmo > 0 && !reloading)
        {
            currentAmmo--;
            ShootWeapon();
            hud.ReduceAmmoCount(ammoPerShot);
        }

        if (currentAmmo >= 1 && !reloading)
        {
            AudioManager.Play(AudioClipName.PistolShot);
        }

        if (currentAmmo <= 0 && !reloading)
        {
            AudioManager.Play(AudioClipName.PistolEmpty);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void ShootWeapon()
    {
        var projectile = Instantiate(projectilePrefab, transform.position,
            Quaternion.identity);

        var bulletScript = projectile.GetComponent<PistolProjectile>();

        bulletScript.MoveToTarget(direction);
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


    IEnumerator ReloadSound()
    {
        yield return new WaitForSeconds(1f);

        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
