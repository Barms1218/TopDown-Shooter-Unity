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
    [SerializeField] private string targetTag;
    private Rigidbody2D rb2d;
    private IDamageable damageable;
    private WaitForSeconds staggerTime;
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        staggerTime = new WaitForSeconds(attackCooldown);
        damageable = target.GetComponent<IDamageable>();
    }

    public void Attack()
    {
        Debug.Log("Attacking player from " + gameObject.name);
        damageable.DealDamage(-damage);
        AudioManager.Play(AudioClipName.MeleeAttack);
        //StartCoroutine(Stagger());
    }

    private IEnumerator Stagger()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return staggerTime;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
