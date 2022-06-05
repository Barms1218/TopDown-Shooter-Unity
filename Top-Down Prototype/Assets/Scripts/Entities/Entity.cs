using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected InputController input = null;
    [SerializeField]
    protected float _health = 100;


    // Movement
    protected Rigidbody2D _body;
    protected Vector2 _direction;
    protected Vector2 _velocity;
    protected Vector2 desiredVelocity;
    [SerializeField, Range(0f, 100f)]
    protected float maxSpeed = 4f;

    protected bool facingRight = true;

    #endregion

    #region Properties  



    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        if (transform.position.x > 0.01)
        {
            facingRight = true;
        }
        else if (transform.position.x < -.001)
        {
            facingRight = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected abstract void GetInput();

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            TakeDamage(collider.gameObject.GetComponent<Projectile>().WeaponDamage);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }


    protected virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
    protected abstract void Die();
}
