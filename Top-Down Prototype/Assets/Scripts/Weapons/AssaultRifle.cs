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

    public override void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(data.ReloadSpeed);

        currentAmmo = data.MaxAmmo;
        hud.CurrentAmmo = currentAmmo;       
        reloading = false;
        AudioManager.Play(AudioClipName.AR_Finish_Reload);
    }
}
