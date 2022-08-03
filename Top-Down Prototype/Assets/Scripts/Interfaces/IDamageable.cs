using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Armor { get; }
    string DamageResistance { get; }
    bool CanTakeDamage { get; set; }

    public void DealDamage(float amount, GameObject damageSource);
}
