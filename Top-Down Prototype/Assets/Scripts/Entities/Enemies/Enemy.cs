using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected Vector3 newPosition;
    protected GameObject _player;
    protected Animator _animator;
    protected Collider2D _collider;

    protected bool facingRight = true;
    protected bool canMove = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (canMove)
        {
            _animator.SetBool("Running", true);
            transform.position = Vector2.MoveTowards(transform.position,
                _player.transform.position, 3f * Time.deltaTime);

            if (_player.transform.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (_player.transform.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }
    }

    protected virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
}
