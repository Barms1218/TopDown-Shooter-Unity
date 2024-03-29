using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    private MovementSM _sm;
    private float horizontalInput;
    private float verticalInput;


    public Moving(MovementSM stateMachine) : base("Moving", stateMachine) 
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        // horizontalInput = 0f;
        // verticalInput = 0f;
        _sm._animator.SetBool("Running", true);
    }

    public override void UpdateLogic()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Mathf.Abs(horizontalInput) < Mathf.Epsilon
        || Mathf.Abs(verticalInput) < Mathf.Epsilon)
        {
            _sm._animator.SetBool("Running", false);
            _sm.ChangeState(_sm.idleState);
        }
    }

    public override void UpdatePhysics()
    {

    }

    public override void Exit()
    {
        
    }
}
