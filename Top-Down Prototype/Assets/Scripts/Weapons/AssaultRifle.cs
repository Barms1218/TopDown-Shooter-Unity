using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Gun
{

    protected override void Start()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);    
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
            currentAmmo--;
            direction.y += Random.Range(-data.Recoil, data.Recoil);
            bulletScript.MoveToTarget(direction.normalized);
            AudioManager.Play(AudioClipName.AR_Fire);
        }
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;
        yield return reloadDelay;
        if (maxAmmo > data.MagazineSize - currentAmmo)
        {
            maxAmmo -= data.MagazineSize - currentAmmo;
            currentAmmo = data.MagazineSize;
        }
        else if (maxAmmo < data.MagazineSize - currentAmmo)
        {
            currentAmmo += maxAmmo;
            maxAmmo = 0;
        }

        UpdateAmmoUI.Instance.UpdateWeaponAmmo(this);      
        reloading = false;
        AudioManager.Play(AudioClipName.ReloadSound);
    }
}
