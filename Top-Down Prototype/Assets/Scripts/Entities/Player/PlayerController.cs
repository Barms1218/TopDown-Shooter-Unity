using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActions actions;

    [SerializeField] float speed;
    private Rigidbody2D _rigidbody2D;
    private bool facingRight = true;
    private GameObject cursor;
    private Vector2 moveDir;
    private bool isDashing = false;

    private void Awake()
    {
        actions = new PlayerActions();
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        moveDir = value.ReadValue<Vector2>().normalized;
        var velocity = _rigidbody2D.velocity;

        velocity.x = moveDir.x * speed;
        velocity.y = moveDir.y * speed;

        _rigidbody2D.velocity = velocity;
    }

    public void OnDash(InputAction.CallbackContext value)
    {
        
        Vector2 dashStart = new Vector2();
        Vector2 dashEnd = new Vector2();
        var dashDistance = 3f;

        if (value.started)
        {
            isDashing = true;
            dashStart = transform.position;
            dashEnd = new Vector2(dashStart.x + dashDistance * moveDir.x,
            dashStart.y);
        }
        else if (value.performed)
        {
            isDashing = false;
            transform.position = dashEnd;
        }
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
        

    }
}
