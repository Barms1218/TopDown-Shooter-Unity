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
        Debug.Log("Entering idle state");
        enemyMachine.Animator.SetBool("Running", false);
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
