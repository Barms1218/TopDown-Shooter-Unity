using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackObject : ScriptableObject
{
    public abstract void Attack(AIController controller);
}
