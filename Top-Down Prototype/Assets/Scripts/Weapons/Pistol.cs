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
        AudioManager.Play(AudioClipName.PistolStartReload);
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = magazineSize;


        PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);       
        reloading = false;
    }
}
