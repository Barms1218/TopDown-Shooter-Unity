using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFlippable
{
    #region Fields
    
    [SerializeField]
    GameObject gun;
    [SerializeField]
    Transform gunTransform;
    [SerializeField]
    List<GameObject> weapons = new List<GameObject>();
    int gunIndex = 0;
    LayerMask weaponLayer;

    // Movement
    Rigidbody2D _body;
    Vector2 _direction;
    Vector2 _velocity;
    Vector2 desiredVelocity;
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 4f;
    bool facingRight = true;

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
        weapons.Add(gun);
    }
    void Update()
    {
        GetInput();

        _velocity = _body.velocity;

        _velocity.x = Mathf.MoveTowards(_velocity.x, desiredVelocity.x, 5f);
        _velocity.y = Mathf.MoveTowards(_velocity.y, desiredVelocity.y, 5f);

        _body.velocity = _velocity;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.position;
        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }

        WeaponSwap();
        PickUp();
    }

    void FixedUpdate()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
        desiredVelocity = new Vector2(_direction.x, _direction.y)
            * Mathf.Max(maxSpeed, 0f);
    }


    void PickUp()
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
                weapons.Add(hit.collider.gameObject);
                weapons[gunIndex].transform.SetParent(gameObject.transform);
                weapons[gunIndex].transform.position = gunTransform.position;

                gun.SetActive(false);
                gun = weapons[gunIndex];
                if (gun.transform.position.x < transform.position.x)
                {
                    Vector3 newScale = gun.transform.localScale;
                    newScale.x *= -1;
                    gun.transform.localScale = newScale;
                }
                weapons[gunIndex].GetComponent<Weapon>().enabled = true;
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
                    gun = weapons[0];
                    weapons[0].SetActive(true);
                }
                catch (System.Exception exception)
                {
                    Debug.Log(exception);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                try
                {
                    gun.SetActive(false);
                    gun = weapons[1];
                    weapons[1].SetActive(true);
                }
                catch(System.Exception exception)
                {
                    gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                try
                {
                    gun.SetActive(false);
                    gun = weapons[2];
                    weapons[2].SetActive(true);
                }
                catch(System.Exception exception)
                {
                    gun.SetActive(true);
                }
            }
        }

    void GetInput()
    {
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

    void Die()
    {
        //state = State.STATE_DYING;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        //state = State.STATE_DEAD;
        gameObject.SetActive(false);
    }
}
