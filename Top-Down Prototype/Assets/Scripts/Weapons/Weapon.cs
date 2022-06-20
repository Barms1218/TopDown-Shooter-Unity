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
    protected bool reloading = false;
    protected HUD hud;
    Vector3 mousePos;
    protected bool facingRight;
    protected float nextFire;

    #endregion

    public int MaxAmmo => data.MaxAmmo;
    public float TimeBetweenShots => data.FireRate;
    public int CurrentAmmo => currentAmmo;

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
    /// 
    /// </summary>
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

    /// <summary>
    /// Allow for semi-automatic fire or automatic fire depending
    /// on the weapon's time between shots.
    /// </summary>
    public abstract void Fire(Vector2 direction);

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

        currentAmmo = data.MaxAmmo;
        hud.DisplayAmmo(currentAmmo, data.MaxAmmo);        
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

    protected virtual void OnEquip(int remainingAmmo)
    {
        
    }
    /// <summary>
    /// 
    /// </summary>
    protected virtual void OnEnable()
     {
        hud.DisplayAmmo(currentAmmo, data.MaxAmmo);
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
