using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    private void Awake()
    {
        reloadDelay = new WaitForSeconds(reloadSpeed);    
    }

    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            bullet = ObjectPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = muzzleTransform.transform.position;
                bullet.transform.rotation = muzzleTransform.transform.rotation;
                bullet.SetActive(true);
            }
            var bulletScript = bullet.GetComponent<Projectile>();
            currentAmmo -= 1;
            direction.y += Random.Range(-recoil, recoil);
            bulletScript.MoveToTarget(direction);
            AudioManager.Play(AudioClipName.PistolShot);
            Debug.Log(currentAmmo);
        }
    }

    public override void SpecialAttack()
    {

    }

    protected override IEnumerator StartReload()
    {
        reloading = true;

        yield return reloadDelay;
        currentAmmo = magazineSize;

        AudioManager.Play(AudioClipName.ReloadSound);
        PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);
        reloading = false;
    }

}
