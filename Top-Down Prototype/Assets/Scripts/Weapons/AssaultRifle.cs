using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{

    protected override void Awake()
    {
        reloadDelay = new WaitForSeconds(reloadSpeed);    
    }

    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            bullet = RiflePool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(
                    muzzleTransform.position, muzzleTransform.rotation);
                bullet.SetActive(true);
                bullet.tag = gameObject.tag;
            }
            var bulletScript = bullet.GetComponent<Projectile>();
            currentAmmo -= AmmoPerShot;
            direction.y += Random.Range(-recoil, recoil);
            bulletScript.MoveToTarget(direction);
            AudioManager.Play(AudioClipName.AR_Fire);
        }
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;
        yield return reloadDelay;
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

        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentAmmo, maxAmmo);      
        reloading = false;
        AudioManager.Play(AudioClipName.ReloadSound);
    }
}
