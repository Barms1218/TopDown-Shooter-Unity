using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    GameObject gun;
    [SerializeField]
    Transform gunTransform;
    [SerializeField]
    List<GameObject> weapons = new List<GameObject>();
    int gunIndex = 0;
    private bool hasAssaultRifle;
    private bool hasShotGun;

    // Events
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

        WeaponSwap();
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
        // Pick up new weapon, add it to the list and equip it
        if (collision.gameObject.tag == "Assault Rifle")
        {
            hasAssaultRifle = true;
            gunIndex++;
            gun.SetActive(false);
            //collision.gameObject.transform.SetParent(gameObject.transform);
            //weapons.Add(collision.gameObject);
            gun = weapons[gunIndex];
            //weapons[gunIndex].transform.position = gunTransform.position;
            //weapons[gunIndex].GetComponent<Weapon>().enabled = true;
            //weapons[gunIndex].GetComponent<BoxCollider2D>().enabled = false;
            weapons[gunIndex].SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Shotgun")
        {
            hasShotGun = true;
            gunIndex++;
            gun.SetActive(false);
            gun = weapons[gunIndex];
            weapons[gunIndex].SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            try
            {
                gun.SetActive(false);
                gun = weapons[0];
                weapons[0].SetActive(true);
            }
            catch(System.Exception exception)
            {
                Debug.Log(exception);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hasAssaultRifle)
            {
                gun.SetActive(false);
                gun = weapons[1];
                weapons[1].SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (hasShotGun)
            {
                gun.SetActive(false);
                gun = weapons[2];
                weapons[2].SetActive(true);
            }
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
