using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon, IInteractable
{
    [SerializeField] int numProjectiles = 5;
    [SerializeField] float pelletSpread = 0.5f;
    private bool firing;

    public override void Fire(Vector2 direction, Weapon weapon)
    {
        base.Fire(direction, weapon);
        for (int i = 0; i < numProjectiles; i++)
        {
            var pellet = Instantiate(projectilePrefab, muzzleTransform.position, 
                Quaternion.identity);

            var pelletScript = pellet.GetComponent<PelletProjectile>();
            direction.y -= Random.Range(-pelletSpread, pelletSpread);
            pelletScript.MoveToTarget(direction);
        }
        AudioManager.Play(AudioClipName.ShotgunBlast);
        firing = true;
    }

    public override void Reload()
    {
        firing = false;
        StartCoroutine(StartReload());
    }

    protected override IEnumerator StartReload()
    {
        reloading = true;

        while (currentAmmo < data.MaxAmmo && !firing)
        {
            currentAmmo++;
            hud.CurrentAmmo++;
            AudioManager.Play(AudioClipName.ShotgunReload);
            yield return new WaitForSeconds(data.ReloadSpeed);
            reloading = false;
        }
    }

    public override void SpecialAttack()
    {

    }
}
