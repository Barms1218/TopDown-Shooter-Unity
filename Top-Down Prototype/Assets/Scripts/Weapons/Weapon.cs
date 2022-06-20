using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Fields

    Rigidbody2D body;
    [SerializeField] protected GunData data;
    protected int currentAmmo;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform muzzleTransform;
    [SerializeField] InputController input;
    protected bool reloading = false;
    protected HUD hud;
    protected Vector3 direction;
    Vector3 mousePos;
    protected bool facingRight;
    protected float nextFire;

    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        hud = FindObjectOfType<HUD>();
        body = GetComponent<Rigidbody2D>();
        currentAmmo = data.MaxAmmo;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {

        Aim();
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Aim()
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
    public virtual void Fire()
    {
        if (Time.time >= nextFire && currentAmmo > 0)
        {
            hud.ReduceAmmoCount(data.AmmoPerShot);
            currentAmmo -= data.AmmoPerShot;
            ShootWeapon();
            nextFire = Time.time + data.FireRate;
        }
        else
        {
            return;
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
        yield return new WaitForSeconds(data.ReloadSpeed);
        hud.SetMaxAmmoCount(data.MaxAmmo);
        currentAmmo = data.MaxAmmo;
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
        hud.SetMaxAmmoCount(data.MaxAmmo);
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
