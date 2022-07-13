using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    EnemySM enemyMachine;
    Vector2 newPosition;

    public EnemyIdle(EnemySM enemySM) : base("Enemy Idle", enemySM)
    {
        enemyMachine = enemySM;
    }

    public override void Enter()
    {
        enemyMachine.Animator.SetBool("Running", false);
        enemyMachine.ChangeState(enemyMachine.chaseState);
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

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
    }
}
