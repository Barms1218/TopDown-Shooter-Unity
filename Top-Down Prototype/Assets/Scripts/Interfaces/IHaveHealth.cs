using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth
{
    float Health { get; set; }
    int MaxHealth{ get; }
    void ReduceHealth(int amount, GameObject damageSource);
    void RestoreHealth(int amount);
}

