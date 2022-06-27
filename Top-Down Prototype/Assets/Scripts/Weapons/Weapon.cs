using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IFlippable, IInteractable
{
    #region Fields

    [SerializeField] protected GunData data;
    protected int currentAmmo;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform muzzleTransform;
    protected bool reloading = false;
    protected HUD hud;
    protected bool facingRight;
    protected PlayerWeaponHandler weaponHandler;

    #endregion

    #region Properties

    public int MaxAmmo => data.MaxAmmo;
    public float TimeBetweenShots => data.FireRate;
    public int CurrentAmmo => currentAmmo;
    public int AmmoPerShot => data.AmmoPerShot;

    #endregion

    protected virtual void Awake()
    {
        hud = FindObjectOfType<HUD>();
        weaponHandler = FindObjectOfType<PlayerWeaponHandler>();
        currentAmmo = data.MaxAmmo;
    }


    public virtual void Aim(float angle, Transform target)
    {
        if (target.position.x < transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (target.position.x > transform.position.x && facingRight)
        {
            Flip();
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    // Virtual because all guns expend ammo the same way
    public abstract void Fire(Vector2 direction);

    public abstract void SpecialAttack();

    public abstract void Reload();

    protected abstract IEnumerator StartReload();

    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    protected virtual void Interact()
    {
        weaponHandler.Gun.SetActive(false);        
        weaponHandler.WeaponList.Add(this.gameObject);
        transform.SetParent(weaponHandler.transform);
        transform.position = weaponHandler.Gun.transform.position;
        weaponHandler.Gun = this.gameObject;      
        GetComponent<Weapon>().enabled = true;
        
        // Flip the weapon to proper scale to match player's
        if (this.gameObject.transform.position.x < weaponHandler.transform.position.x)
        {
            Vector3 newScale = weaponHandler.Gun.transform.localScale;
            newScale.x *= -1;
            weaponHandler.Gun.transform.localScale = newScale;
        }          
        weaponHandler.CurrentWeapon = GetComponent<Weapon>();
        weaponHandler.Gun.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        hud.MaxAmmo = weaponHandler.CurrentWeapon.MaxAmmo;
        hud.CurrentAmmo = weaponHandler.CurrentWeapon.currentAmmo;
    }
}
