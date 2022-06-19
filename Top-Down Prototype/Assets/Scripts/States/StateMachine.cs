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
