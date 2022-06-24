using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeZombie : Enemy
{
    private CircleCollider2D attackCircle;
    private float attackRadius;
    LayerMask layerMask;
    protected override void Awake()
    {
        base.Awake();
        this.Points = 15;
        layerMask = LayerMask.GetMask("Entities");
    }

    protected override void Attack()
    {
        if (CanAttack)
        {
            var _direction = target.transform.position - transform.position;
            if (player.GetComponent<IDamageable>() != null)
            {
                player.GetComponent<IDamageable>().TakeDamage(settings.Damage, this.gameObject);
            }
            nextAttack = Time.time + 1f;
        }      
    }

    private IEnumerator StaggerTime()
    {
        var stagger = 0f;

        while (stagger < 1)
        {
            stagger += Time.deltaTime;
            GetComponent<EnemyMove>().enabled = false;
            yield return null;
        }
        GetComponent<EnemyMove>().enabled = true;
    }
}
