using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Move
{
    [SerializeField] AIController _controller;
    [SerializeField] private float chaseDistance;

    protected override void Start()
    {
        lookAtTransform = PlayerController.player.transform;
    }

    public override void MoveObject(Vector2 input)
    {
        var _distance = Vector2.Distance(input, transform.position);
        if (input != null && _distance > chaseDistance)
        {
            _controller.Animator.SetBool("Running", true);
            Vector2 movePosition = transform.position;
            movePosition = Vector2.MoveTowards(transform.position,
                input, speed * Time.deltaTime);
            _controller.Rigidbody2D.MovePosition(movePosition);
        }
    }

    private void OnEnable()
    {
        _controller.moveDelegate += MoveObject;
    }

    private void OnDisable()
    {
        _controller.moveDelegate -= MoveObject;
    }
}
