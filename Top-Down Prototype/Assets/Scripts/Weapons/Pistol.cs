using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{

    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            bullet = PistolPool.SharedInstance.GetPooledObject();
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
            AudioManager.Play(AudioClipName.PistolShot);
        }
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;

        yield return reloadDelay;
        data.CurrentAmmo = data.MagazineSize;

        AudioManager.Play(AudioClipName.ReloadSound);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(data.CurrentAmmo, data.MaxAmmo);
        reloading = false;
    }

}
