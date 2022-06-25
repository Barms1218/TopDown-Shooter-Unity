using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private float nextAttack;
    private float recoverTime;
    private Enemy settings;
    GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        settings = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            var _distance = Vector2.Distance(player.transform.position, transform.position);
            if (_distance < settings.AttackRange)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        var _health = player.GetComponent<IDamageable>();
        if (CanAttack() && _health != null)
        {
            _health.TakeDamage(settings.Damage, this.gameObject, settings.AttackStrength);
            nextAttack = Time.time + settings.Cooldown;
            StartCoroutine(RecoverFromAttack());
        }
    }

    private IEnumerator RecoverFromAttack()
    {
        recoverTime = 0f;
        
        while (recoverTime < 1)
        {
            recoverTime += Time.deltaTime;
            GetComponent<EnemyMove>().enabled = false;
            yield return null;
        }
        GetComponent<EnemyMove>().enabled = true;
    }

    private bool CanAttack() => Time.time >= nextAttack;
}
