using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    GameObject currentWeapon;

    // Events
    public delegate void AttackInput();
    public static event AttackInput OnShoot;
    public delegate void SpecialInput();
    public static event SpecialInput OnSpecial;
    public delegate void ReloadInput();
    public static event ReloadInput OnReload;


    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        GetInput();

        _velocity = _body.velocity;

        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, 5f);
        _velocity.y = Mathf.MoveTowards(_velocity.y, desiredVelocity.y, 5f);

        _body.velocity = _velocity;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        _direction.x = input.RetrieveHorizontalInput();
        _direction.y = input.RetrieveVerticalInput();
        desiredVelocity = new Vector2(_direction.x, _direction.y)
            * Mathf.Max(maxSpeed, 0f);

    }

    protected override void GetInput()
    {
        if (input.RetrieveShootInput())
        {
            OnShoot?.Invoke();
        }
        if (input.MouseHeldDown())
        {
            OnSpecial?.Invoke();
        }
        if (input.RetrieveReloadInput())
        {
            OnReload?.Invoke();
        }
    }


    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
