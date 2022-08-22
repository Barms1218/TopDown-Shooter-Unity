using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [SerializeField] private AttackObject meleeAttack;
    private IDamageable damageable;
    private WaitForSeconds staggerTime;
    private GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        staggerTime = new WaitForSeconds(meleeAttack.AttackCoolDown);
        damageable = target.GetComponent<IDamageable>();
    }

    public void Attack()
    {
        damageable.DealDamage(-meleeAttack.Damage);
        AudioManager.Play(meleeAttack.AttackSound);
        //StartCoroutine(Stagger());
    }

    //private IEnumerator Stagger()
    //{
    //    rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

    //    yield return staggerTime;
    //    rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    //}
}
