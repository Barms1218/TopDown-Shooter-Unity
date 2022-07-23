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
    [SerializeField] float dashTime;
    [SerializeField] float dashCoolDown;
    private bool facingRight = true;
    private bool canDash = true;
    private WaitForSeconds dashSeconds;
    private WaitForSeconds coolDownSeconds;
    
    private Vector2 moveDir;

    private void Awake()
    {
        dashSeconds = new WaitForSeconds(dashTime);
        coolDownSeconds = new WaitForSeconds(dashCoolDown);
    }

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
        if (moveDir.magnitude != 0)
        {
            _animator.SetBool("Running", true);
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

    public void Dash()
    {
        Debug.Log("Dashing");
        if (canDash)
        {
            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        var savedSpeed = speed;
        speed *= 2f;
        canDash = false;
        yield return dashSeconds;
        speed = savedSpeed;
        yield return coolDownSeconds;
        canDash = true;
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
}
