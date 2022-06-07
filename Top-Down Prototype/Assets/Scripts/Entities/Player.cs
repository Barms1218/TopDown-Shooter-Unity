using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    GameObject gun;
    [SerializeField]
    Transform gunTransform;

    Weapon weapon;
    // Events
    public delegate void SpecialInput();
    public static event SpecialInput OnSpecial;
    public delegate void ReloadInput();
    public static event ReloadInput OnReload;


    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<Weapon>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Weapon>() != null)
        {
            gun.SetActive(false);
            collision.gameObject.transform.SetParent(gameObject.transform);
            gun = collision.gameObject;
            gun.transform.position = gunTransform.position;
            gun.SetActive(true);
            collision.GetComponent<Weapon>().enabled = true;
            collision.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    protected override void GetInput()
    {
        base.GetInput();
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnSpecial?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }
    }


    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
