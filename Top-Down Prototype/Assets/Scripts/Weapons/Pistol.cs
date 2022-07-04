using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon, IInteractable
{

    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
                Quaternion.identity);
            var flash = Instantiate(muzzleFlashPrefab, muzzleTransform.position,
             transform.rotation);
            Destroy(flash, timeBetweenShots / 5);
            var bulletScript = projectile.GetComponent<Projectile>();
            currentAmmo -= AmmoPerShot;
            bulletScript.MoveToTarget(direction);
            AudioManager.Play(AudioClipName.PistolShot);
        }
    }

    public override void SpecialAttack()
    {
        while(true)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
        }
    }
    protected override IEnumerator StartReload()
    {
        reloading = true;

        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = magazineSize;

        AudioManager.Play(AudioClipName.ReloadSound);
        PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);       
        reloading = false;
    }
}
