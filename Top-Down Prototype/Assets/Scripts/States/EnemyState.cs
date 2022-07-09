using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{

    protected abstract void Enter();
    protected abstract void Update();
    protected abstract void Exit();
}
