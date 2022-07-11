using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth
{
    int Health { get; set; }
    int MaxHealth{ get; }
    void TakeDamage(int amount, GameObject damageSource, float attackStrength);
    void RestoreHealth(int amount);
    bool IsDying => Health <= 0;
}

