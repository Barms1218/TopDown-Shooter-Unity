using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] GameObject child;
    [SerializeField] float speed;
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

        if (SetTheCursor.cursor.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (SetTheCursor.cursor.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        if (moveDir.magnitude != 0)
        {
            controller.Animator.SetBool("Running", true);
        }
    }

    public void Movement(Vector2 input)
    {
        moveDir = input;

        var velocity = controller.Rigidbody2D.velocity;

        velocity.x = moveDir.x * speed;
        velocity.y = moveDir.y * speed;
        controller.Animator.SetBool("Running", true);
        controller.Rigidbody2D.velocity = velocity;


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
        Vector3 newScale = child.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        child.transform.localScale = newScale;
    }
}
