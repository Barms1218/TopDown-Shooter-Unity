using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D _rigidbody2D;
    private bool facingRight = true;
    private GameObject cursor;


    private void Awake()
    {
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
        var moveDir = value.ReadValue<Vector2>();
        var velocity = _rigidbody2D.velocity;

        velocity.x = moveDir.x * speed;
        velocity.y = moveDir.y * speed;

        _rigidbody2D.velocity = velocity;
    }

    public void OnDash(InputAction.CallbackContext value)
    {


    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
        

    }
    private void OnEnable()
    {
        //moveAction = inputs.PlayerControls.Movement;
        //attackAction = inputs.PlayerControls.Fire;
    }

    private void OnDisable()
    {
        //moveAction.Disable();
        //attackAction.Disable();
    }

}
