using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRifle : Weapon
{


    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void Fire()
    {
        if (currentAmmo > 0 && !reloading)
        {
            StartCoroutine(ContinuousFire());

        }
    }

    IEnumerator ContinuousFire()
    {
        yield return null;

        ShootWeapon();
        hud.ReduceAmmoCount(ammoPerShot);
    }

    protected override void ShootWeapon()
    {
        var projectile = Instantiate(projectilePrefab, transform.position,
            Quaternion.identity);

        var bulletScript = projectile.GetComponent<PistolProjectile>();

        bulletScript.MoveToTarget(direction);
    }

    protected override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }
}
