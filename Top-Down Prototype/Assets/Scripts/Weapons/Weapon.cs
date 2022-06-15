using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields

    Rigidbody2D body;

    [SerializeField]
    protected int currentAmmo;
    [SerializeField]
    protected int maxAmmo;
    [SerializeField]
    protected float reloadSpeed;
    [SerializeField]
    protected float timeBetweenShots;
    [SerializeField]
    protected int ammoPerShot;
    [SerializeField]
    protected bool reloading = false;
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected Transform muzzleTransform;

    protected HUD hud;
    protected Vector3 direction;
    Vector3 mousePos;
    protected bool facingRight;
    protected bool flipped;
    protected float nextFire;
    Coroutine continousFire;
    private bool empty;

    #endregion

    #region Properties

    protected int CurrentAmmo
    {
        get => currentAmmo;
        set => currentAmmo = value;
    }

    public int MaxAmmo => maxAmmo;
    protected float ReloadSpeed => reloadSpeed;
    protected float TimeBetweenShots => timeBetweenShots;
    protected int AmmoPerShot => ammoPerShot;
    protected bool Reloading => reloading;

    #endregion

    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        hud = FindObjectOfType<HUD>();
        body = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Aim();

        Fire();
    }

    /// <summary>
    /// 
    /// </summary>
    void Aim()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -Camera.main.transform.position.z;
        direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mousePos.x < transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && facingRight)
        {
            Flip();
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    /// <summary>
    /// Allow for semi-automatic fire or automatic fire depending
    /// on the weapon's time between shots.
    /// </summary>
    protected virtual void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFire)
        {
            if (currentAmmo > 0)
            {
                hud.ReduceAmmoCount(ammoPerShot);
                currentAmmo -= ammoPerShot;
                ShootWeapon();
                nextFire = Time.time + timeBetweenShots;
            }
        }
    }


    protected abstract void ShootWeapon();
    protected abstract void SpecialAttack();

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    /// <summary>
    /// Put gun into reloading state with boolean
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        hud.SetMaxAmmoCount(maxAmmo);
        currentAmmo = maxAmmo;
        reloading = false;
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void OnEnable()
     {
        hud.SetMaxAmmoCount(maxAmmo);
        Player.OnSpecial += SpecialAttack;
        Player.OnReload += Reload;
     }

     /// <summary>
     /// 
     /// </summary>
    protected virtual void OnDisable()
    {
        Player.OnSpecial -= SpecialAttack;
        Player.OnReload -= Reload;
    }
}
