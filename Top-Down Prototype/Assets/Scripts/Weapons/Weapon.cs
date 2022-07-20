using System.Collections; using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    #region Fields

    protected GameObject bullet;
    [SerializeField] protected Transform muzzleTransform;

    [Header("Ammunition Stats")]
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int magazineSize;
    [SerializeField] protected int ammoPerShot;

    [Header("Gun Properties")]
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float reloadSpeed;
    [SerializeField,Range(0, 1f)] protected float recoil;
    [SerializeField] bool canRapidFire;
    protected bool reloading = false;
    protected bool facingRight;
    protected WaitForSeconds reloadDelay;

    #endregion

    #region Properties

    public int MaxAmmo { get { return maxAmmo; } set { maxAmmo = value; } }
    public float TimeBetweenShots => timeBetweenShots;
    public int CurrentAmmo => currentAmmo;
    public int AmmoPerShot => ammoPerShot;
    public bool FacingRight => FacingRight;
    public bool CanRapidFire => canRapidFire;

    #endregion

    public void Aim(float angle, Transform targetTransform)
    {
        if (targetTransform.position.x < transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (targetTransform.position.x > transform.position.x && facingRight)
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
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }
    protected abstract IEnumerator StartReload();

    public void Flip()
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
}
