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
    protected bool facingRight;

    Coroutine continousFire;

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

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hud = FindObjectOfType<HUD>();
        body = GetComponent<Rigidbody2D>();
        hud.SetMaxAmmoCount(MaxAmmo);
    }

    protected virtual void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -Camera.main.transform.position.z;
        direction = mousePos - transform.position;

        float deltaX = mousePos.x - transform.position.x;
        float deltaY = mousePos.y - transform.position.y;

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;

        Fire();

        if (mousePos.x > transform.position.x && 
            facingRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x &&
            !facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Allow for semi-automatic fire or automatic fire depending
    /// on the weapon's time between shots.
    /// </summary>
    protected virtual void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            continousFire = StartCoroutine(ContinuousFire());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(continousFire);
        }
    }

    protected abstract IEnumerator ContinuousFire();
    protected abstract void ShootWeapon();
    protected abstract void SpecialAttack();
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
    /// Change the weapon's local scale to face direction of the mouse
    /// </summary>
    protected virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;
        newScale.y *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    protected virtual void OnEnable()
     {
        Player.OnSpecial += SpecialAttack;
        Player.OnReload += Reload;
     }
    protected virtual void OnDisable()
    {
        Player.OnSpecial -= SpecialAttack;
        Player.OnReload -= Reload;
    }
}
