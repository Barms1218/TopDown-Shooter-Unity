﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] TrailRenderer trail;

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessHit(collision.gameObject);
    }


    private void ProcessHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out IDamageable damageable)
            && !hitObject.CompareTag(gameObject.tag))
        {
            damageable.DealDamage(damage, gameObject);
        }
        gameObject.SetActive(false);
        trail.Clear();
    }

    public virtual void MoveToTarget(Vector2 force)
    {
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0, 0, angle);

            transform.rotation = target;
            rigidbody2D.AddForce(force.normalized *
                speed, ForceMode2D.Impulse);
        }

    }
}
