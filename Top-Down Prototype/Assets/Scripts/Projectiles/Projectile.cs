using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields
    private float timeToLive;
    private Weapon weapon;
    [SerializeField] private ProjectileData projectileData;

    #endregion

    #region Properties

    public float WeaponDamage => projectileData.Damage;

    #endregion

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Health>()?.TakeDamage(projectileData.Damage);
        Destroy(gameObject);
    }

    public void MoveToTarget(Vector2 force)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;
        body.AddRelativeForce(force.normalized * 
            projectileData.AmountOfForce, ForceMode2D.Impulse);
    }
}
