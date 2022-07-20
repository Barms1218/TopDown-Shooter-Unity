using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] int numProjectiles = 5;
    private bool firing;


    private void Awake()
    {
        reloadDelay = new WaitForSeconds(reloadSpeed);
    }

    public override void Fire(Vector2 direction)
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            bullet = ShotgunBulletPool.ShotGunInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = muzzleTransform.transform.position;
                bullet.transform.rotation = muzzleTransform.transform.rotation;
                bullet.SetActive(true);
            }

            var projectileScript = bullet.GetComponent<Projectile>();
            direction.y += Random.Range(-recoil, recoil);
            projectileScript.MoveToTarget(direction);
        }
        currentAmmo -= ammoPerShot;
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

        while (currentAmmo < magazineSize && !firing && maxAmmo > 0)
        {
            currentAmmo++;
            maxAmmo--;
            PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);
            //AudioManager.Play(AudioClipName.ShotgunReload);
            AudioManager.Play(AudioClipName.ReloadSound);
            yield return new WaitForSeconds(reloadSpeed);
            reloading = false;
        }
    }

    public override void SpecialAttack()
    {

    }
}
