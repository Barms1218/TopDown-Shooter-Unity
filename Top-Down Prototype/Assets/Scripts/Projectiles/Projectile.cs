using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    
    [SerializeField]
    int damage;
    [SerializeField]
    float timeToLive;
    [SerializeField]
    float forceMagnitude;

    #endregion

    void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitObject = collision.gameObject;
        if (hitObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DealDamage(damage, gameObject);
        }
        Destroy(gameObject);
    }

    public void MoveToTarget(Vector2 force)
    {
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0, 0, angle);

            transform.rotation = target;
            rigidbody2D.AddRelativeForce(force.normalized *
                forceMagnitude, ForceMode2D.Impulse);
        }

    }
}
