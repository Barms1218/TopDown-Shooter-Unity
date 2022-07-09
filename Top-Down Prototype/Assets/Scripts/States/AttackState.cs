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
        Debug.Log("Entering attack state");
        var _health = enemyMachine.Player.GetComponent<IHaveHealth>();
        if (_health != null)
        {
            AudioManager.Play(AudioClipName.MeleeAttack);
            _health.TakeDamage(enemyMachine.Damage, enemyMachine.gameObject, enemyMachine.AttackStrength);
            //StartCoroutine(RecoverFromAttack());
            //nextAttack = Time.time + attackCooldown;
            enemyMachine.StartCoroutine(RecoverFromAttack());
            
        }
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
