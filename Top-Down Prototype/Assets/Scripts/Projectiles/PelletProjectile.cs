using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletProjectile : MonoBehaviour
{
    [SerializeField]
    ProjectileData projectileData;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(projectileData.Damage);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="force"></param>
    public void MoveToTarget(Vector2 force)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        body.AddForce(force.normalized * 
            projectileData.AmountOfForce, ForceMode2D.Impulse);
    }
}
