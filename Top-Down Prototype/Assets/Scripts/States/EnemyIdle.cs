using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState
{
    EnemySM enemyMachine;
    Vector2 newPosition;
    float maxDistance = 2f;
    float moveTimer = 0;
    float moveAgainTime = 2f;

    public EnemyIdle(EnemySM enemySM) : base("Enemy Idle", enemySM)
    {
        enemyMachine = enemySM;
    }

    public override void Enter()
    {
        Debug.Log("Entering idle state");
        enemyMachine.Animator.SetBool("Running", false);
        ChangePosition();
        //enemyMachine.StartCoroutine(Wander());
    }

    public override void UpdateLogic()
    {
        // enemy wanders around randomly
        if (!enemyMachine.CanSeePlayer())
        {
            enemyMachine.transform.position = Vector2.MoveTowards(
                enemyMachine.transform.position, newPosition, 1f * Time.deltaTime);
            if (Time.time >= moveTimer)
            {
                ChangePosition();
                Debug.Log(newPosition);
                moveTimer = Time.time + moveAgainTime;
            }

            if (newPosition.x > enemyMachine.transform.position.x && !enemyMachine.FacingRight)
            {
                enemyMachine.Flip();
            }
            else if (newPosition.x < enemyMachine.transform.position.x && enemyMachine.FacingRight)
            {
                enemyMachine.Flip();
            }
        }
        else
        {
            enemyMachine.ChangeState(enemyMachine.chaseState);
        }

    }

    public override void UpdatePhysics()
    {
        
    }
    public override void Exit()
    {

    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance));
    }
}
