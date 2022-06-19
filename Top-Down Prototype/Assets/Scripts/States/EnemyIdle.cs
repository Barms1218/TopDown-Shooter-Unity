using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    private EnemySM enemySM;
    private Transform target;

    public EnemyIdle(EnemySM enemyMachine) : base("Enemy Idle", enemyMachine)
    {
        enemySM = enemyMachine;
    }

    public override void Enter()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void UpdateLogic()
    {
        if (Vector2.Distance(enemySM.transform.position, target.position) > 2f)
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
