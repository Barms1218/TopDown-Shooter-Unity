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
            var pellet = Instantiate(projectilePrefab, muzzleTransform.position, 
                Quaternion.identity);

            var pelletScript = pellet.GetComponent<PelletProjectile>();
            direction.y -= Random.Range(-pelletSpread, pelletSpread);
            pelletScript.MoveToTarget(direction);
        }
        currentAmmo -= AmmoPerShot;
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
            PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, MaxAmmo);
            AudioManager.Play(AudioClipName.ShotgunReload);
            yield return new WaitForSeconds(data.ReloadSpeed);
            reloading = false;
        }
    }

    public override void SpecialAttack()
    {

    }
}
