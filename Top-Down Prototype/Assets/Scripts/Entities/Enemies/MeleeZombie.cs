using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeZombie : Enemy
{
    private CircleCollider2D attackCircle;
    private float attackRadius;

    protected override void Start()
    {
        base.Start();
        Points = 15;
        //attackCircle = GetComponent<CircleCollider2D>();
        //attackRadius = attackCircle.radius;
    }

    protected override void Attack()
    {
        player.GetComponent<Health>().TakeDamage(10);
    }
}
