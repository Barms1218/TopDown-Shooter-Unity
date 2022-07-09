using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    EnemySM enemyMachine;


    public EnemyIdle(EnemySM enemySM) : base("Enemy Idle", enemySM)
    {
        enemyMachine = enemySM;
    }

    public override void Enter()
    {
        enemyMachine.Animator.SetBool("Running", false);
    }

    public override void UpdateLogic()
    {
        if (enemyMachine.CanSeePlayer())
        {
            stateMachine.ChangeState(enemyMachine.chaseState);
        }
    }

    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {

    }


}
