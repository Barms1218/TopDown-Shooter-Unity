using System.Collections; using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour, IInteractable
{
    #region Fields

    [Header("Prefabs")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject muzzleFlashPrefab;
    [SerializeField] protected Transform muzzleTransform;
    [Header("Ammunition Stats")]
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int magazineSize;
    [SerializeField] protected int ammoPerShot;
    [Header("Gun Properties")]
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float reloadSpeed;
    protected bool reloading = false;
    protected bool facingRight;
    public static UnityAction OnReload;
    public static UnityAction<GameObject> OnPickUp;

    #endregion

    #region Properties

    public int MaxAmmo { get { return maxAmmo; } set { maxAmmo = value; } }
    public float TimeBetweenShots => timeBetweenShots;
    public int CurrentAmmo => currentAmmo;
    public int AmmoPerShot => ammoPerShot;
    public bool FacingRight => FacingRight;

    #endregion

    protected virtual void Awake()  
    {

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
    protected abstract IEnumerator StartReload();

    public virtual void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.y *= -1;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    protected virtual void AddAmmo(int _amount, string weaponName)
    {

    }
    public virtual void Interact() => OnPickUp?.Invoke(gameObject);
}
