using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private EnemySM enemySM;
    private Transform target;

    public AttackState(EnemySM stateMachine) : base("Attack", stateMachine)
    {
        enemySM = stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Attacking now");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {
        
    }
}
