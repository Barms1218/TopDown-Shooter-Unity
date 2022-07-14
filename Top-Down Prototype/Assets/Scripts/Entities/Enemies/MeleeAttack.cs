using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int attackStrength;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float nextAttack;
    private GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (player != null)
        {
            var _distance = Vector2.Distance(player.transform.position, transform.position);
            if (_distance <= attackRange)
                Attack();
        }

    } 

    void Attack()
    {
        var _health = player.GetComponent<IHaveHealth>();
        if (CanAttack() && _health != null)
        {
            AudioManager.Play(AudioClipName.MeleeAttack);
            _health.TakeDamage(damage, gameObject, attackStrength);
            nextAttack = Time.time + attackCooldown;
        }
    }

    private bool CanAttack() => Time.time >= nextAttack;
}
