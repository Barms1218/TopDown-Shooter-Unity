using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected Animator _animator;
    protected GameObject _player;

    protected bool facingRight = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (_player.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }
    void FixedUpdate()
    {
        _animator.SetBool("Running", true);
        Vector2 movePosition = transform.position;
        movePosition = Vector2.MoveTowards(transform.position,
            _player.transform.position, speed * Time.deltaTime);
        rb2d.MovePosition(movePosition);
    }

    protected virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
}
