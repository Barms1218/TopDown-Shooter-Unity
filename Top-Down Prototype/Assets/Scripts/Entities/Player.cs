using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlippable
{
    #region Fields
    
    [SerializeField] GameObject gun;
    List<GameObject> weaponList = new List<GameObject>();
    float timeToTriggerPull;
    Vector2 direction;

    // Movement
    Rigidbody2D _body;
    Vector2 _direction;
    Vector2 _velocity;
    Vector2 desiredVelocity;
    [SerializeField, Range(0f, 100f)] float maxSpeed = 4f;
    bool facingRight = true;
    Weapon currentWeapon;

    #endregion
    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }
    public List<GameObject> WeaponList => weaponList;
    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }
    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        PlayerInput.OnReload += Reload;
        PlayerInput.OnFire += Fire;
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        GetInput();

        _velocity = _body.velocity;

        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, 5f);
        _velocity.y = Mathf.MoveTowards(_velocity.y, desiredVelocity.y, 5f);

        _body.velocity = _velocity;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        currentWeapon.Aim(angle);
    }

    void FixedUpdate()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
        desiredVelocity = new Vector2(_direction.x, _direction.y)
            * Mathf.Max(maxSpeed, 0f);
    }

    
    void GetInput()
    {
        // if (Input.GetMouseButton(0) && currentWeapon.CurrentAmmo > 0
        // && Time.time >= timeToTriggerPull)
        // {
        //     currentWeapon.Fire(direction);
        //     timeToTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
        // }
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentWeapon.SpecialAttack();
        }
    }

    /// <summary>
    /// Have player character face the direction of the
    /// mouse cursor
    /// </summary>
    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    void Reload()
    {
        currentWeapon.Reload();
    }

    public void Fire()
    {
        if (currentWeapon.CurrentAmmo > 0
        && Time.time >= timeToTriggerPull)
        {
            currentWeapon.Fire(direction);
            timeToTriggerPull = Time.time + currentWeapon.TimeBetweenShots;            
        }
    }
}
