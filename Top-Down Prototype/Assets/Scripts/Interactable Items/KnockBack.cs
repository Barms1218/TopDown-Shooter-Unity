using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void ProcessHit(GameObject collision)
    {
        var hitObject = collision;

        if (hitObject.CompareTag("Enemy"))
        {
            var rb2d = hitObject.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                rb2d.AddForce(transform.position);
            }
            //if (hitObject.TryGetComponent(out Rigidbody2D rigidbody))
            //{
            //    Debug.Log("I've been hit");
            //    rigidbody.AddForce(transform.position - hitObject.transform.position
            //        * 30, ForceMode2D.Impulse);
            //}
        }
    }
}
