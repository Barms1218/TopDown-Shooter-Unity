using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float nextAttack;
    private EnemySettings settings;
    GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        settings = GetComponent<EnemySettings>();
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    // Update is called once per frame
    void Update()
    {
        if (settings.Attacktarget != null)
        {
            var _distance = Vector2.Distance(settings.Attacktarget.transform.position, transform.position);
            if (_distance < settings.AttackRange)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        var _health = settings.Attacktarget.GetComponent<IDamageable>();
        if (CanAttack() && _health != null)
        {
            
        }
    }

    private bool CanAttack() => Time.time >= nextAttack;
}
