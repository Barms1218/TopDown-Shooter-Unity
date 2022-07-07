using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon, IInteractable
{
    [SerializeField] int numProjectiles = 5;
    [SerializeField,Range(0, 2f)] float pelletSpread = 0.5f;
    private bool firing;

    public override void Fire(Vector2 direction)
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            var projectile = Instantiate(projectilePrefab, muzzleTransform.position, 
                Quaternion.identity);

            var projectileScript = projectile.GetComponent<Projectile>();
            direction.y -= Random.Range(-pelletSpread, pelletSpread);
            projectileScript.MoveToTarget(direction);
        }
        var flash = Instantiate(muzzleFlashPrefab, muzzleTransform.position,
         transform.rotation);
        Destroy(flash, timeBetweenShots / 5);
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
