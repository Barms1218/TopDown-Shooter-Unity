using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeZombie : Enemy
{
    private CircleCollider2D attackCircle;
    private float attackRadius;

    protected override void Awake()
    {
        base.Awake();
        this.Points = 15;
    }

    protected override void Attack()
    {
        if (CanAttack)
        {
            player.GetComponent<IDamageable>().TakeDamage(settings.Damage);
            Debug.Log(settings.Damage);
            nextAttack = Time.time + 1f;
        }
    }
}
