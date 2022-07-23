using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator _animator;
    [SerializeField] private float chaseDistance;
    private GameObject player;
    private bool facingRight = true;

    private void Start()
    {
        player = PlayerController.player.gameObject;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x >
                transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (player.transform.position.x <
                transform.position.x && facingRight)
            {
                Flip();
            }
        }
    }
    void FixedUpdate()
    {
        var _distance = Vector2.Distance(player.transform.position,
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
                player.transform.position, speed * Time.deltaTime);
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
