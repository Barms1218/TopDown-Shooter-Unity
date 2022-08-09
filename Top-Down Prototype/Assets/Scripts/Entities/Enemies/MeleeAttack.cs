using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Attack", menuName = "AI Attacks/Melee")]
public class MeleeAttack : AttackObject
{
    [SerializeField] private int attackStrength;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private Rigidbody2D rb2d;
    private WaitForSeconds staggerTime;
    private GameObject player;

    private void Start()
    {
        player = PlayerController.playerInstance.gameObject;
        staggerTime = new WaitForSeconds(attackCooldown);
    }

    public override void Attack(AIController controller)
    {
        player.GetComponent<IDamageable>().DealDamage(-damage);
    }

    private IEnumerator Stagger()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return staggerTime;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
