using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;


    enum State
    {
        STATE_IDLE,
        STATE_MOVE,
        STATE_HURT,
        STATE_DYING,
        STATE_DEAD,
    }
    State state;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        currentState = GetInitialState();
        currentState?.Enter();
    }
    void Update()
    {
        currentState?.UpdateLogic();
        // switch (state)
        // {
        //     case State.STATE_IDLE:
        //         _animator.SetBool("Running", false);
        //         if (_body.velocity.x != 0)
        //         {
        //             state = State.STATE_MOVE;
        //         }
        //         break;
        //         case State.STATE_MOVE:
        //         _animator.SetBool("Running", true);
        //         if (_body.velocity == Vector2.zero)
        //         {
        //             state = State.STATE_IDLE;
        //         }
        //         break;
        //         case State.STATE_DYING:
        //         _animator.SetTrigger("Dying");
        //         break;
        //         case State.STATE_HURT:
        //         _animator.SetTrigger("Hurt");
        //         break;
        // }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        currentState?.UpdatePhysics();
    }
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
