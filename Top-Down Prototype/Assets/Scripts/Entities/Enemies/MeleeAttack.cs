using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int attackStrength;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] AIController _controller;
    private WaitForSeconds staggerTime;
    private GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        player = PlayerController.player.gameObject;
        staggerTime = new WaitForSeconds(attackCooldown);
        //_controller.attackDelegate += Attack;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitObject = collision.gameObject;
        if (!hitObject.CompareTag(gameObject.tag))
        {
            ProcessHit(hitObject);
        }
    }

    private void ProcessHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DealDamage(damage, gameObject);
            StartCoroutine(Stagger());
        }
        else
        {
            Debug.Log("I can't damage that target");
        }
    }

    private IEnumerator Stagger()
    {
        _controller.enabled = false;

        yield return staggerTime;

        _controller.enabled = true;
    }
}
