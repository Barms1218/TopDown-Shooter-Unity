using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawProjectile : Projectile
{
    CircleCollider2D circleCollider;



    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        RaycastHit2D hit = Physics2D.Raycast(circleCollider.bounds.center, Vector2.right,
            circleCollider.radius);

        if (hit.collider != null)
        {
            Debug.DrawLine(circleCollider.bounds.center, hit.point, Color.red);
        }    

    }
}
