using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameObject walkParticle;
    [SerializeField]
    GameObject dashParticle;
    [SerializeField]
    Transform particleTransform;
    private Rigidbody2D _body;
    private Animator _animator;
    private Vector2 _direction, _velocity, desiredVelocity;
    private float dashTimer = 1.0f;
    private float dashCoolDown;
    private float dashDistance = 3f;
    private bool isDashing;
    private Vector2 dashStart, dashEnd;

    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    
    
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _velocity = _body.velocity;

        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, 5f);
        _velocity.y = Mathf.MoveTowards(_velocity.y, desiredVelocity.y, 5f);

        _body.velocity = _velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashing = true;
            dashStart = transform.position;
            dashEnd = new Vector2(dashStart.x + dashDistance * _direction.x, 
            dashStart.y);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isDashing = false;
        }
    }

    private void FixedUpdate()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
        var speed = Mathf.Abs(_direction.x + _direction.y);
        if (speed > 0)
        {
            var dust = Instantiate(walkParticle,
                particleTransform.position, particleTransform.rotation);
            Destroy(dust, 0.2f);
        }
        _animator.SetFloat("Speed", speed);
        desiredVelocity = new Vector2(_direction.x, _direction.y)
            * Mathf.Max(maxSpeed, 0f);


        if (isDashing && CanDash)
        {
            // incrementing time
            float currentDashTime = 0;

            // updating position
            transform.position = Vector2.Lerp(dashStart, dashEnd, dashTimer);
            currentDashTime += Time.deltaTime;
            if (currentDashTime >= dashTimer)
            {
                // dash finished
                isDashing = false;
                transform.position = dashEnd;
            }
            dashCoolDown = Time.time + 2f;
        }
     
    }

    private bool CanDash => Time.time >= dashCoolDown;
}
