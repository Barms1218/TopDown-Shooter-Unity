using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(IHaveHealth))]
public class IDamageable : MonoBehaviour
{

    IHaveHealth health;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        health = GetComponent<IHaveHealth>();
    }

    public void DealDamage(float amount, GameObject damageSource)
    {
        health.ReduceHealth(amount, damageSource);
    }
}
