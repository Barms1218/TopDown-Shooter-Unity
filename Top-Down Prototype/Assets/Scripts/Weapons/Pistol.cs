using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            var bulletScript = Instantiate(projectilePrefab, muzzleTransform.position,
                Quaternion.identity).GetComponent<Projectile>();
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

        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = magazineSize;

        AudioManager.Play(AudioClipName.ReloadSound);
        PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);
        reloading = false;
    }

}
