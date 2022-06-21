using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private EnemySM enemySM;
    Transform target;
    [SerializeField] float speed = 3f;
    

    public ChaseState(EnemySM enemyMachine) : base("Chase", enemyMachine)
    {
        enemySM = enemyMachine;
    }

    public override void Enter()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemySM._animator.SetBool("Running", true);
    }

    public override void UpdateLogic()
    {
        enemySM.transform.position = Vector2.MoveTowards(
            enemySM.transform.position, target.position, Time.deltaTime * enemySM.enemySettings.Speed);
    }

    public override void UpdatePhysics()
    {
        
    }

    public override void Exit()
    {
        
    }
}
