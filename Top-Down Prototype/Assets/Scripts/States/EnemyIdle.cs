using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    EnemySM enemyMachine;
    private GameObject[] wayPoints;

    public EnemyIdle(EnemySM enemySM) : base("Enemy Idle", enemySM)
    {
        enemyMachine = enemySM;
    }

    public override void Enter()
    {
        enemyMachine.Animator.SetBool("Running", false);
        wayPoints = GameEnvironment.WayPoints;
        Debug.Log(wayPoints.Length);
    }

    public override void UpdateLogic()
    {

        if (enemyMachine.CanSeePlayer())
        {
            enemyMachine.ChangeState(enemyMachine.chaseState);
        }
        else
        {
            enemyMachine.transform.position = Vector2.MoveTowards(
                enemyMachine.transform.position, wayPoints[2].transform.position, Time.deltaTime * 3f);
            
        }
    }

    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {

    }


}
