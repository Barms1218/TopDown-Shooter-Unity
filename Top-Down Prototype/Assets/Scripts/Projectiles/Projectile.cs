using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields

    [SerializeField]
    public float damage;
    [SerializeField]
    protected float forceMagnitude = 10f;
    [SerializeField]
    protected float timeToLive = 5f;

    #endregion

    #region Properties

    public float WeaponDamage => damage;

    #endregion

    protected virtual void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void MoveToTarget(Vector2 force)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        body.AddForce(force.normalized * forceMagnitude, ForceMode2D.Impulse);
    }
}
