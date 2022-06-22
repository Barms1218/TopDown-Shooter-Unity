using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IFlippable
{
    #region Fields

    [SerializeField] protected GunData data;
    protected int currentAmmo;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform muzzleTransform;
    protected bool reloading = false;
    protected HUD hud;
    Vector3 mousePos;
    protected bool facingRight;

    #endregion

    #region Properties

    public int MaxAmmo => data.MaxAmmo;
    public float TimeBetweenShots => data.FireRate;
    public int CurrentAmmo => currentAmmo;

    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        hud = FindObjectOfType<HUD>();
        currentAmmo = data.MaxAmmo;
    }

    // Takes an angle from the player so it can be aimed
    public virtual void Aim(float angle)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

    // Virtual because all guns expend ammo the same way
    public virtual void Fire(Vector2 direction)
    {
        currentAmmo -= data.AmmoPerShot;
        hud.CurrentAmmo = currentAmmo;
    }

    public abstract void SpecialAttack();

    public abstract void Reload();

    protected abstract IEnumerator StartReload();

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
        hud.CurrentAmmo = currentAmmo;
        hud.MaxAmmo = data.MaxAmmo;
     }
}
