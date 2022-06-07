using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    protected override void Fire()
    {
        base.Fire();
        if (currentAmmo <= 0)
        {
            AudioManager.Play(AudioClipName.PistolEmpty);
        }
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
                AudioManager.Play(AudioClipName.PistolShot);

            }

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void ShootWeapon()
    {
        var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
            Quaternion.identity);

        var bulletScript = projectile.GetComponent<Projectile>();

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
        StartCoroutine(ReloadSound());
        AudioManager.Play(AudioClipName.PistolStartReload);

    }


    IEnumerator ReloadSound()
    {
        yield return new WaitForSeconds(reloadSpeed);

        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
