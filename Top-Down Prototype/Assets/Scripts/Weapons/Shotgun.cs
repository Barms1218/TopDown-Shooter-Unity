using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] int numProjectiles = 5;
    private bool firing;


    protected override void Awake()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);
    }

    public override void Fire(Vector2 direction)
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            bullet = ShotgunPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(
                    muzzleTransform.position, muzzleTransform.rotation);
                bullet.SetActive(true);
                bullet.tag = gameObject.tag;
            }

            var projectileScript = bullet.GetComponent<Projectile>();
            direction.y += Random.Range(-data.Recoil, data.Recoil);
            projectileScript.MoveToTarget(direction.normalized);
        }
        data.CurrentAmmo--;
        AudioManager.Play(AudioClipName.ShotgunBlast);
        firing = true;
    }

    public override void Reload()
    {
        firing = false;
        base.Reload();
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;

        while (data.CurrentAmmo < data.MagazineSize && !firing && data.MaxAmmo > 0)
        {
            data.CurrentAmmo++;
            data.MaxAmmo--;
            UpdateAmmoUI.Instance.UpdateWeaponAmmo(data.CurrentAmmo, data.MaxAmmo);
            AudioManager.Play(AudioClipName.ReloadSound);
            yield return reloadDelay;
            reloading = false;
        }
    }
}
