using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private EnemySM enemySM;

    public AttackState(EnemySM stateMachine) : base("Attack", stateMachine)
    {
        enemySM = stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Attacking now");
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
