using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    float timeToLive;
    [SerializeField]
    ProjectileData projectileData;

    #endregion

    #region Properties

    public float WeaponDamage => projectileData.Damage;

    #endregion

    void Start()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float deltaX = mousePos.x - transform.position.x;
        float deltaY = mousePos.y - transform.position.y;

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;

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
            collision.gameObject.GetComponent<Health>().TakeDamage(projectileData.Damage);
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

        body.AddRelativeForce(force.normalized * 
            projectileData.AmountOfForce, ForceMode2D.Impulse);
    }
}
