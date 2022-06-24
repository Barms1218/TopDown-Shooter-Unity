using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using System;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject player;
    protected bool facingRight = true;
    protected int points;
    protected Transform target;
    protected float nextAttack;
    [SerializeField] protected EnemySettings settings;

    protected virtual int Points
    {
        set
        {
            points = value;
        }
        get => points;
    }

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    protected virtual void Update()
    {
        MoveToPlayer();

        if (Vector2.Distance(target.position, transform.position) < settings.AttackRange)
        {
            Attack();
        }

        if (player.transform.position.x > transform.position.x
            && !facingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x
            && facingRight)
        {
            Flip();
        }
    }

    protected virtual void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
        target.position, Time.deltaTime * settings.Speed);        
    }

    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    protected abstract void Attack();

    protected bool CanAttack => Time.time >= nextAttack;
}
