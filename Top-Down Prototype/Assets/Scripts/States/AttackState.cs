using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    EnemySM enemyMachine;
    private float attackCooldown = 1f;

    public AttackState(EnemySM stateMachine) : base("Attack", stateMachine)
    {
        enemyMachine = stateMachine;
    }

    public override void Enter()
    {
        enemyMachine.StartCoroutine(RecoverFromAttack());
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

    private IEnumerator RecoverFromAttack()
    {
        yield return new WaitForSeconds(attackCooldown);

        enemyMachine.ChangeState(enemyMachine.chaseState);
    }
}
