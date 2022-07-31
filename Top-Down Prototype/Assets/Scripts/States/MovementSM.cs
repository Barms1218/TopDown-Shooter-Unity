using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving moveState;
    public Rigidbody2D rb2d;
    [SerializeField, Range(0, 20)]
    public int speed;
    public Animator _animator;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        idleState = new Idle(this);
        moveState = new Moving(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
