using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon, IInteractable
{

    /// <summary>
    /// 
    /// </summary>
    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            var projectile = Instantiate(projectilePrefab, muzzleTransform.position,
                Quaternion.identity);

            var bulletScript = projectile.GetComponent<Projectile>();
            currentAmmo -= AmmoPerShot;
            bulletScript.MoveToTarget(direction);
            AudioManager.Play(AudioClipName.AR_Fire);
        }
    }

    public override void SpecialAttack()
    {
        return;
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        if (maxAmmo > magazineSize - currentAmmo)
        {
            maxAmmo -= magazineSize - currentAmmo;
            currentAmmo = magazineSize;
        }
        else if (maxAmmo < magazineSize - currentAmmo)
        {
            currentAmmo += maxAmmo;
            maxAmmo -= maxAmmo;
        }

        PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);       
        reloading = false;
        AudioManager.Play(AudioClipName.AR_Finish_Reload);
    }
}
