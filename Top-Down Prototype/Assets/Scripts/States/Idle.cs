using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState
{
    private MovementSM _sm;
    
    private float horizontalInput;
    private float verticalInput;
    private Animator _animator;
    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) 
    {
        _sm = stateMachine;
    }


    public override void Enter()
    {
        horizontalInput = 0f;
        verticalInput = 0f;
    }

    public override void UpdateLogic()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Mathf.Abs(horizontalInput) > Mathf.Epsilon
        || Mathf.Abs(verticalInput) > Mathf.Epsilon)
        {
            stateMachine.ChangeState(_sm.moveState);
        }
    }

    public override void UpdatePhysics()
    {
       
    }

    public override void Exit()
    {
        
    }
}
