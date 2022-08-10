using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private int armor;
    [SerializeField] Health health;
    [SerializeField] bool canTakeDamage = true;


    public int Armor => armor;
    public bool CanTakeDamage
    {
        get => canTakeDamage;
        set { canTakeDamage = value; }
    }

    public void DealDamage(int amount)
    {
        if (canTakeDamage)
        {
            if (armor <= 0)
            {
                health.ChangeHealth(amount);
            }
            else
            {
                armor--;
            }
        }
    }
}
