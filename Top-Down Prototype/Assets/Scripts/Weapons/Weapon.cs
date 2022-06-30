using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour, IFlippable, IInteractable
{
    #region Fields

    [SerializeField] protected GunData data;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform muzzleTransform;
    protected int currentAmmo;
    protected int maxAmmo;
    protected int magazineSize;
    protected bool reloading = false;
    protected bool facingRight;
    public static UnityAction OnReload;
    public static UnityAction<GameObject> OnPickUp;

    #endregion

    #region Properties

    public int MaxAmmo => maxAmmo;
    public float TimeBetweenShots => data.FireRate;
    public int CurrentAmmo => currentAmmo;
    public int AmmoPerShot => data.AmmoPerShot;

    #endregion

    protected virtual void Awake()  
    {
        currentAmmo = data.MagazineSize; 
        maxAmmo = data.MaxAmmo;
        magazineSize = data.MagazineSize;
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

    public abstract void Fire(Vector2 direction);

    public abstract void SpecialAttack();

    public virtual void Reload()
    {
        if (!reloading && maxAmmo > 0)
        {
            StartCoroutine(StartReload());
        }
    }
    protected virtual IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(data.ReloadSpeed);
        if (maxAmmo > magazineSize - currentAmmo)
        {
            maxAmmo -= magazineSize - currentAmmo;
            currentAmmo = magazineSize;
        }
        else if (maxAmmo < magazineSize - currentAmmo)
        {
            currentAmmo += maxAmmo;
            maxAmmo -= maxAmmo;
        }

        PlayerWeaponHandler.SetAmmoCount?.Invoke(currentAmmo, maxAmmo);       
        reloading = false;
    }
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    protected virtual void Interact() => OnPickUp?.Invoke(gameObject);
}
