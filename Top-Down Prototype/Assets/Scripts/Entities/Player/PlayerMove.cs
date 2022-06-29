using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D _body;
    private Vector2 _direction;
    private Vector2 _velocity;
    private Vector2 desiredVelocity;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    
    
    void Awake() => _body = GetComponent<Rigidbody2D>();

    void Update()
    {
        _velocity = _body.velocity;

        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, 5f);
        _velocity.y = Mathf.MoveTowards(_velocity.y, desiredVelocity.y, 5f);

        _body.velocity = _velocity;        
    }

    private void FixedUpdate()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
        var speed = Mathf.Abs(_direction.x + _direction.y);
        GetComponent<Animator>().SetFloat("Speed", speed);
        desiredVelocity = new Vector2(_direction.x, _direction.y)
            * Mathf.Max(maxSpeed, 0f);        
    }
}
