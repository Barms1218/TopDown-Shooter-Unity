using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlippable
{
    #region Fields
    
    [SerializeField] GameObject gun;
    [SerializeField] Transform gunTransform;
    List<GameObject> weaponList = new List<GameObject>();
    int gunIndex = 0;
    LayerMask weaponLayer;
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

    // Events
    public delegate void SpecialInput();
    public static event SpecialInput OnSpecial;
    public delegate void ReloadInput();
    public static event ReloadInput OnReload;

    #endregion

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        weaponLayer = LayerMask.GetMask("Weapons");
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
        WeaponSwap();
        PickUpGun();
        
        currentWeapon.Aim(angle);
    }

    void FixedUpdate()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
        desiredVelocity = new Vector2(_direction.x, _direction.y)
            * Mathf.Max(maxSpeed, 0f);
    }

    /// <summary>
    /// 
    /// </summary>
    void PickUpGun()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, weaponLayer);
        Color lineColor;
        
        lineColor = Color.red;
        if (hit.collider != null)
        {
            Debug.Log("Press E to pick up");
            if (Input.GetKeyDown(KeyCode.E))
            {
                gunIndex++;
                weaponList.Add(hit.collider.gameObject);
                weaponList[gunIndex].transform.SetParent(gameObject.transform);
                weaponList[gunIndex].transform.position = gunTransform.position;

                gun.SetActive(false);
                gun = weaponList[gunIndex];
                if (gun.transform.position.x < transform.position.x)
                {
                    Vector3 newScale = gun.transform.localScale;
                    newScale.x *= -1;
                    gun.transform.localScale = newScale;
                }
                weaponList[gunIndex].GetComponent<Weapon>().enabled = true;
                currentWeapon = weaponList[gunIndex].GetComponent<Weapon>();
                hit.collider.enabled = false;
            }
            else
            {
                lineColor = Color.green;
            }

            
        }
        Debug.DrawRay(transform.position, direction, lineColor);
    }

    void WeaponSwap()
        {
            Vector3 newScale = gun.transform.localScale;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                try
                {
                    gun.SetActive(false);
                    gun = weaponList[0];
                    weaponList[0].SetActive(true);
                    currentWeapon = weaponList[0].GetComponent<Weapon>();
                }
                catch (System.Exception exception)
                {
                    Debug.Log(exception);
                    gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                try
                {
                    gun.SetActive(false);
                    gun = weaponList[1];
                    weaponList[1].SetActive(true);
                    currentWeapon = weaponList[1].GetComponent<Weapon>();
                }
                catch(System.Exception exception)
                {
                    Debug.Log(exception);
                    gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                try
                {
                    gun.SetActive(false);
                    gun = weaponList[2];
                    weaponList[2].SetActive(true);
                    currentWeapon = weaponList[2].GetComponent<Weapon>();
                }
                catch(System.Exception exception)
                {
                    Debug.Log(exception);
                    gun.SetActive(true);
                }
            }
        }
    
    void GetInput()
    {
        if (Input.GetMouseButton(0) && currentWeapon.CurrentAmmo > 0
        && Time.time >= timeToTriggerPull)
        {
            currentWeapon.Fire(direction);
            timeToTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
            Debug.Log(currentWeapon.CurrentAmmo);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnSpecial?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }
}
