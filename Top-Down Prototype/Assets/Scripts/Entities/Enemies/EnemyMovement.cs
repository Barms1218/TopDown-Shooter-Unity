using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator _animator;
    [SerializeField] private float chaseDistance;

    protected bool facingRight = true;


    // Update is called once per frame
    protected virtual void Update()
    {
        if (PlayerController.player != null)
        {
            if (PlayerController.player.transform.position.x >
                transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (PlayerController.player.transform.position.x <
                transform.position.x && facingRight)
            {
                Flip();
            }
        }
    }
    void FixedUpdate()
    {
        var _distance = Vector2.Distance(PlayerController.player.transform.position,
            transform.position);
        if (_distance > chaseDistance)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (PlayerController.player != null)
        {
            _animator.SetBool("Running", true);
            Vector2 movePosition = transform.position;
            movePosition = Vector2.MoveTowards(transform.position,
                PlayerController.player.transform.position, speed * Time.deltaTime);
            rb2d.MovePosition(movePosition);
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
