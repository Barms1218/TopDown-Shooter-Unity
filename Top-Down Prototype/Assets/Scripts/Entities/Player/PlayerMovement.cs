using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Move
{
    //[SerializeField] PlayerController _controller;
    //[SerializeField] float dashTime;
    //[SerializeField] float dashCoolDown;
    //private bool canDash = true;
    //private WaitForSeconds dashSeconds;
    //private WaitForSeconds coolDownSeconds;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    dashSeconds = new WaitForSeconds(dashTime);
    //    coolDownSeconds = new WaitForSeconds(dashCoolDown);
    //}

    //protected override void Start()
    //{
    //    lookAtTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
    //}


    //public override void MoveObject(Vector2 input)
    //{
    //    var moveDir = input;
    //    rb2d.MovePosition(rb2d.position + speed * Time.deltaTime * moveDir);
    //    _controller.Animator.SetBool("Running", true);
    //}

    //public void Dash()
    //{
    //    if (canDash)
    //    {
    //        StartCoroutine(Dashing());
    //    }
    //}

    //private IEnumerator Dashing()
    //{
    //    var savedSpeed = speed;
    //    speed *= 2f;
    //    canDash = false;
    //    yield return dashSeconds;
    //    speed = savedSpeed;
    //    yield return coolDownSeconds;
    //    canDash = true;
    //}

    //private void OnEnable()
    //{
    //    _controller.dashDelegate += Dash;
    //}

    //private void OnDisable()
    //{
    //    _controller.dashDelegate -= Dash;
    //}
}
