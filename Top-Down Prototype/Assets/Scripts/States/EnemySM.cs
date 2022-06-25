using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public EnemyIdle idleState;
    [HideInInspector]
    public AttackState attackState;

    // cached components
    public Rigidbody2D rb2d;
    public Animator _animator;
    public Enemy enemySettings;

    private void Awake()
    {
        chaseState = new ChaseState(this);
        idleState = new EnemyIdle(this);
        attackState = new AttackState(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
