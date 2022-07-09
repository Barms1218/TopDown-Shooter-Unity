using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCombatState : BaseState
{
    EnemySM enemyMachine;

    public InCombatState(string name, EnemySM stateMachine) : base(name, stateMachine)
    {
        enemyMachine = stateMachine;
    }

    public override void Enter()
    {
        if (enemyMachine.GetComponent<EnemyWeaponHandler>() != null)
        {

        }
        else
        {

        }
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateLogic()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdatePhysics()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
