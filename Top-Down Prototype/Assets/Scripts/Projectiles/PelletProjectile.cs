using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletProjectile : MonoBehaviour
{
    [SerializeField] ProjectileData projectileData;
    float timeToLive;

    void Start()
    {
        timeToLive = projectileData.TimeToLive;
    }
    void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        IHaveHealth damageable = other.gameObject.GetComponent<IHaveHealth>();
        if (damageable != null)
        {
            damageable.TakeDamage(projectileData.Damage, this.gameObject, 
            projectileData.ProjectileForce);
        }
        Destroy(gameObject);       
    }


    public void MoveToTarget(Vector2 force)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        body.AddForce(force.normalized * projectileData.AmountOfForce, 
        ForceMode2D.Impulse);
    }
}
