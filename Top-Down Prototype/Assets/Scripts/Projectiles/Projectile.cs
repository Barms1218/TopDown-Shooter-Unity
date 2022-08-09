using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    
    [SerializeField] int damage;
    [SerializeField] float timeToLive = 0.5f;
    [SerializeField] float elapsedTime;
    [SerializeField] float speed;
    [SerializeField] TrailRenderer trail;

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessHit(collision.gameObject);
    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void ProcessHit(GameObject hitObject)
    {
        if (hitObject.TryGetComponent(out IDamageable damageable)
            && !hitObject.CompareTag(gameObject.tag))
        {
            damageable.DealDamage(-damage);
            gameObject.SetActive(false);

            AudioManager.Play(AudioClipName.BulletHit);
        }
        var bloodSplatter = BloodPool.SharedInstance.GetPooledObject();
        if (bloodSplatter != null && hitObject.TryGetComponent(out Health health))
        {
            bloodSplatter.transform.SetPositionAndRotation(
                transform.position, transform.rotation);
            bloodSplatter.SetActive(true);
        }

    }

    public virtual void MoveToTarget(Vector2 force)
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;
        rigidbody2D.AddRelativeForce(force * speed, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        elapsedTime = timeToLive;
    }

    private void OnDisable()
    {
        trail.Clear();
    }
}
