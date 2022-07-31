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
        Debug.Log("Entering chase state");
    }

    public override void UpdateLogic()
    {

        if (enemyMachine.Player != null)
        {
            enemyMachine.transform.position = Vector2.MoveTowards(enemyMachine.transform.position,
            enemyMachine.Player.transform.position, Time.deltaTime * speed);

            if (enemyMachine.Player.transform.position.x > enemyMachine.transform.position.x
            && !enemyMachine.FacingRight)
            {
                enemyMachine.Flip();
            }
            else if (enemyMachine.Player.transform.position.x < enemyMachine.transform.position.x
                && enemyMachine.FacingRight)
            {
                enemyMachine.Flip();
            }
        }



    }

    public override void UpdatePhysics()
    {
        
    }

    public override void Exit()
    {
        
    }

}
