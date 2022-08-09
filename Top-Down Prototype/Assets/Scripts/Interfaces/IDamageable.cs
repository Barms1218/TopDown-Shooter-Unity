using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Armor { get; }
    bool CanTakeDamage { get; set; }

    public void DealDamage(int amount);
}
