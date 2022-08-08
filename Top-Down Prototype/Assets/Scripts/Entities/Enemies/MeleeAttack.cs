using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [SerializeField] private int attackStrength;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private Rigidbody2D rb2d;
    private WaitForSeconds staggerTime;
    private GameObject player;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = PlayerController.playerInstance.gameObject;
        staggerTime = new WaitForSeconds(attackCooldown);
    }

    public void Attack()
    {
        //var pushForce = player.transform.position - transform.position;
        player.GetComponent<IDamageable>().DealDamage(damage, gameObject);
        //player.GetComponent<Rigidbody2D>().AddForce(
        //    pushForce.normalized * attackStrength, ForceMode2D.Impulse);
        StartCoroutine(Stagger());
    }

    private IEnumerator Stagger()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return staggerTime;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
