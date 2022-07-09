using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeePlayerState : BaseState
{
    protected float sightRange;
    protected EnemySM enemySM;
    protected GameObject _player;
    protected Collider2D _collider;
    protected LayerMask detectionLayer;
    protected bool seePlayer = false;

    public SeePlayerState(string name, StateMachine stateMachine) : base(name, stateMachine)
    {
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
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
}
