using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float Health { get; set; }
    int MaxHealth{ get; }
    void TakeDamage(int amount, GameObject damageSource);
    void RestoreHealth(int amount);
}

