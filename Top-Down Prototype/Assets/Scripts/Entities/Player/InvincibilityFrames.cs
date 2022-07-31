using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityTakeDamage))]
public class InvincibilityFrames : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] EntityTakeDamage takeDamage;
    [SerializeField] int knockbackStrength = 10;
    [SerializeField] float invincibleTime = 1f;
    private WaitForSeconds invincibility;

    private void Start()
    {
        invincibility = new WaitForSeconds(invincibleTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void ProcessHit(GameObject gameObject)
    {
        var hitObject = gameObject;

        if (hitObject.CompareTag("Enemy"))
        {
            controller.CanMove = false;
            takeDamage.CanTakeDamage = false;
            var pushDirection = transform.position - hitObject.transform.position;
            controller.Rigidbody2D.AddForce(pushDirection * knockbackStrength,
                ForceMode2D.Impulse);
            StartCoroutine(Recover());
        }
    }

    private IEnumerator Recover()
    {
        yield return invincibility;
        controller.CanMove = true;
        takeDamage.CanTakeDamage = true;
    }
}
