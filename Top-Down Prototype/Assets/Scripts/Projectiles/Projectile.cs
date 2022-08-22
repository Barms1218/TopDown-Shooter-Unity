using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields

    [SerializeField] ProjectileData data;
    float elapsedTime;

    TrailRenderer trail;

    #endregion

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void ProcessHit(GameObject hitObject)
    {
        var damageable = hitObject.GetComponent<IDamageable>();
        if (damageable != null && !hitObject.CompareTag(gameObject.tag))
        {
            damageable.DealDamage(-data.Damage);
            gameObject.SetActive(false);

            AudioManager.Play(data.HitClip);
        }
    }

    public virtual void MoveToTarget(Vector2 force)
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;
        rigidbody2D.AddRelativeForce(force * data.Speed, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        elapsedTime = data.TimeToLive;
    }

    private void OnDisable()
    {
        trail.Clear();
    }
}
