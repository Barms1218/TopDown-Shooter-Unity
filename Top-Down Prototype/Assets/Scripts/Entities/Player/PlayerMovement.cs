using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Move
{
    [SerializeField] PlayerController _controller;
    [SerializeField] float dashTime;
    [SerializeField] float dashCoolDown;
    private bool canDash = true;
    private WaitForSeconds dashSeconds;
    private WaitForSeconds coolDownSeconds;

    private void Awake()
    {
        dashSeconds = new WaitForSeconds(dashTime);
        coolDownSeconds = new WaitForSeconds(dashCoolDown);
    }

    protected override void Start()
    {
        lookAtTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
        _controller.moveDelegate += MoveObject;
    }


    public override void MoveObject(Vector2 input)
    {
        var moveDir = input;

        var velocity = _controller.Rigidbody2D.velocity;

        velocity.x = moveDir.x * speed;
        velocity.y = moveDir.y * speed;
        _controller.Animator.SetBool("Running", true);
        _controller.Rigidbody2D.velocity = velocity;
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
}
