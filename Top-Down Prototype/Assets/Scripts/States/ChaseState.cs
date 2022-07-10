using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private EnemySM enemyMachine;

    [SerializeField]
    float speed = 3f;


    public ChaseState(EnemySM enemySM) : base("Chase", enemySM)
    {
        enemyMachine = enemySM;
    }

    public override void Enter()
    {
        enemyMachine.Animator.SetBool("Running", true);
    }

    public override void UpdateLogic()
    {

        enemyMachine.transform.position = Vector2.MoveTowards(enemyMachine.transform.position,
        enemyMachine.Player.transform.position, Time.deltaTime * speed);


        if (enemyMachine.Player.transform.position.x > enemyMachine.transform.position.x
        && !enemyMachine.FacingRIght)
        {
            enemyMachine.Flip();
        }
        else if (enemyMachine.Player.transform.position.x < enemyMachine.transform.position.x
            && enemyMachine.FacingRIght)
        {
            enemyMachine.Flip();
        }

        // transition to idle state
        if (!enemyMachine.CanSeePlayer())
        {
            enemyMachine.ChangeState(enemyMachine.idleState);
        }

        if (Vector2.Distance(enemyMachine.Player.transform.position,
            enemyMachine.transform.position) <= enemyMachine.AttackRange)
        {
            enemyMachine.ChangeState(enemyMachine.attackState);
        }
    }

    public override void UpdatePhysics()
    {
        
    }

    public override void Exit()
    {
        
    }

}
