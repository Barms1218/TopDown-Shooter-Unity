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
    private GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        player = PlayerController.player.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void ProcessHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out IDamageable damageable) &&
            !hitObject.CompareTag(gameObject.tag))
        {
            damageable.DealDamage(damage, gameObject);
            var pushDirection = transform.position - hitObject.transform.position;
            if (TryGetComponent(out Rigidbody2D _body2d))
            {
                _body2d?.AddForce(pushDirection.normalized * attackStrength, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.Log("I can't damage that target");
        }
    }
}
