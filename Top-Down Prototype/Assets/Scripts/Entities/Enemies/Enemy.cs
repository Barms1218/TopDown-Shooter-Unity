using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject player;
    AIPath path;
    protected bool facingRight = true;
    protected StateMachine stateMachine;
    protected int points;
    [SerializeField] protected EnemySettings enemySettings;

    protected int Points
    {
        set
        {
            points = value;
        }
        get => points;
    }


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        // if (Vector2.Distance(player.transform.position, transform.position) < enemySettings.AttackRange)
        // {
        //     Attack();
        // }
        
        // Use boolean to logically decide if flipping is necessary
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


    /// <summary>
    /// 
    /// </summary>
    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    protected abstract void Attack();
}
