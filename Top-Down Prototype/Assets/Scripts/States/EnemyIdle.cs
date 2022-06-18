using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    private EnemySM enemySM;

    public EnemyIdle(EnemySM enemyMachine) : base("Enemy Idle", enemyMachine)
    {
        enemySM = enemyMachine;
    }

    public override void Enter()
    {
        
    }

    public override void UpdateLogic()
    {
        if (enemySM.enemy.path.remainingDistance >= 5)
        {
            enemySM.ChangeState(enemySM.chaseState);
        }
    }

    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {

    }
}
