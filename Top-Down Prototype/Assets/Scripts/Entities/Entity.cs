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
    protected Animator _animator;


    // Movement
    protected Rigidbody2D _body;
    protected Vector2 _direction;
    protected Vector2 _velocity;
    protected Vector2 desiredVelocity;
    [SerializeField, Range(0f, 100f)]
    protected float maxSpeed = 4f;

    protected bool facingRight = true;

    protected State state;

    #endregion

    protected enum State
    {
        STATE_IDLE,
        STATE_MOVE,
        STATE_DYING,
        STATE_DEAD,
    }

    #region Properties  



    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        state = State.STATE_IDLE;

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

    protected virtual void GetInput()
    {
        switch (state)
        {
            case State.STATE_IDLE:
                if (_body.velocity.x != 0)
                {
                    state = State.STATE_MOVE;
                    _animator.SetBool("Running", false);
                }
                break;
                case State.STATE_MOVE:
                _animator.SetBool("Running", true);
                if (_body.velocity.x == 0)
                {
                    state = State.STATE_IDLE;
                }
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collider"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile"
            && _health > 0)
        {
            TakeDamage(collider.gameObject.GetComponent<Projectile>().WeaponDamage);
            Destroy(collider.gameObject);
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
