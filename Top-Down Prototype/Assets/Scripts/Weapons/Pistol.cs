using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    Vector2 direction;

    protected override void Fire()
    {
        base.Fire();
        currentAmmo--;

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
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePos - transform.position;
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
        yield return new WaitForSeconds(reloadSpeed);

        AudioManager.Play(AudioClipName.PistolStopReload);
    }
}
