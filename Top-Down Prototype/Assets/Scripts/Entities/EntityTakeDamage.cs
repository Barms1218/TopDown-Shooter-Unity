using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private int armor;
    [SerializeField] private string damageResistance;
    IHaveHealth health;


    public int Armor => armor;
    public string DamageResistance => damageResistance;

    void Awake()
    {
        health = GetComponent<IHaveHealth>();
    }

    public void DealDamage(float amount, GameObject damageSource)
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
}
