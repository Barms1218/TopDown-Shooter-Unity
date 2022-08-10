using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TakeDamage))]
public class InvincibilityFrames : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] int knockbackStrength = 10;
    [SerializeField] float invincibleTime = 1f;
    private LayerMask normalLayer;
    private IHaveHealth health;
    private WaitForSeconds invincibleSeconds;

    private void Start()
    {
        invincibleSeconds = new WaitForSeconds(invincibleTime);
        health = gameObject.GetComponent<IHaveHealth>();
        normalLayer = gameObject.layer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ProcessHit(collision.gameObject);
    }

    private void ProcessHit(GameObject hitObject)
    {
        //var hitObject = gameObject;

        //if (hitObject.CompareTag("Enemy"))
        //{

        //}
        controller.enabled = false;
        //takeDamage.CanTakeDamage = false;
        var pushDirection = transform.position - hitObject.transform.position;
        GetComponent<Rigidbody2D>().AddForce(pushDirection * knockbackStrength,
            ForceMode2D.Impulse);
        StartCoroutine(Recover());
    }

    private IEnumerator Recover()
    {
        gameObject.layer = LayerMask.GetMask("Pickup");
        yield return invincibleSeconds;
        controller.enabled = true;
        gameObject.layer = normalLayer;
        //takeDamage.CanTakeDamage = true;
    }
}
