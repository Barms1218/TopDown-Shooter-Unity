using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private int armor;
    [SerializeField] private string damageResistance;
    [SerializeField] Health health;
    [SerializeField] bool canTakeDamage = true;


    public int Armor => armor;
    public string DamageResistance => damageResistance;
    public bool CanTakeDamage
    {
        get => canTakeDamage;
        set { canTakeDamage = value; }
    }

    public void DealDamage(float amount, GameObject damageSource)
    {
        if (canTakeDamage)
        {
            if (armor <= 0)
            {
                health.ReduceHealth(amount, damageSource);
            }
            else
            {
                armor--;
            }
        }
        Debug.Log(amount + "damage taken");
    }
}
