using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region Fields

    [SerializeField]
    protected InputController input = null;
    [SerializeField]
    protected int _health = 100;


    // Movement
    protected Rigidbody2D _body;
    protected Vector2 _direction;
    protected Vector2 _velocity;
    protected Vector2 desiredVelocity;
    [SerializeField, Range(0f, 100f)]
    protected float maxSpeed = 4f;

    #endregion

    #region Properties  



    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected abstract void GetInput();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

     public virtual void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
