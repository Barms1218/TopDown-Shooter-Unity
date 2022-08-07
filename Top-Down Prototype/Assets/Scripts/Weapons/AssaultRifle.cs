using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Gun
{

    protected override void Awake()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);    
    }

    public override void Fire   (Vector2 direction)
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
            data.CurrentAmmo -= 1;
            direction.y += Random.Range(-data.Recoil, data.Recoil);
            bulletScript.MoveToTarget(direction.normalized);
            AudioManager.Play(AudioClipName.AR_Fire);
        }
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;
        yield return reloadDelay;
        if (data.MaxAmmo > data.MagazineSize - data.CurrentAmmo)
        {
            data.MaxAmmo -= data.MagazineSize - data.CurrentAmmo;
            data.CurrentAmmo = data.MagazineSize;
        }
        else if (data.MaxAmmo < data.MagazineSize - data.CurrentAmmo)
        {
            data.CurrentAmmo += data.MaxAmmo;
            data.MaxAmmo -= data.MaxAmmo;
        }

        UpdateAmmoUI.Instance.UpdateWeaponAmmo(data.CurrentAmmo, data.CurrentAmmo);      
        reloading = false;
        AudioManager.Play(AudioClipName.ReloadSound);
    }
}
