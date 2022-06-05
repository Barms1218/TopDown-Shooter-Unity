using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolProjectile : Projectile
{
    protected override void Update()
    {

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float deltaX = mousePos.x - transform.position.x;
        float deltaY = mousePos.y - transform.position.y;

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;
    }
}
