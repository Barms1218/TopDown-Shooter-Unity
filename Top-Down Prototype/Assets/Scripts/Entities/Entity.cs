using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IFlippable
{
    #region Fields

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
        STATE_HURT,
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
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetInput();
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void GetInput()
    {
        switch (state)
        {
            case State.STATE_IDLE:
                _animator.SetBool("Running", false);
                if (_body.velocity.x != 0)
                {
                    state = State.STATE_MOVE;
                }
                break;
                case State.STATE_MOVE:
                _animator.SetBool("Running", true);
                if (_body.velocity == Vector2.zero)
                {
                    state = State.STATE_IDLE;
                }
                break;
                case State.STATE_DYING:
                _animator.SetTrigger("Dying");
                break;
                case State.STATE_HURT:
                _animator.SetTrigger("Hurt");
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
    /// <summary>
    /// 
    /// </summary>
    protected abstract void Die();
}
