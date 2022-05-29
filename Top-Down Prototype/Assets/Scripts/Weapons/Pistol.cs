using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    Vector2 direction;

    protected override void Fire()
    {
        base.Fire();
        currentAmmo--;
    }

    protected override void Update()
    {
        base.Update();
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePos - transform.position;
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
        while(true)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
        }
    }

    void SetLinePositions(Vector2 startPos, Vector2 endPos)
    {

    }
}
