using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] Animator _animator;
    private bool facingRight = true;
    
    private Vector2 moveDir;

    private void Update()
    {

        if (cursor.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (cursor.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }

    }

    public void Movement(Vector2 input)
    {
        moveDir = input;
        var velocity = _rigidbody2D.velocity;

        velocity.x = moveDir.x * speed;
        velocity.y = moveDir.y * speed;
        _animator.SetBool("Running", true);
        _rigidbody2D.velocity = velocity;
    }

    public void OnDash()
    {
        _rigidbody2D.AddForce(moveDir * 20, ForceMode2D.Impulse);
        //transform.position = Vector2.Lerp(dashStart, dashEnd, dashTimer);
        //currentDashTime += Time.deltaTime;
        //if (currentDashTime >= dashTimer)
        //{
        //    // dash finished
        //    isDashing = false;
        //    transform.position = dashEnd;
        //}
        //dashCoolDown = Time.time + 2f;
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
}
