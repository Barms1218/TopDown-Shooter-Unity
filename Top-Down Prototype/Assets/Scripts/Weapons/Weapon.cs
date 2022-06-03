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
    protected float maxRange;
    [SerializeField]
    protected int ammoPerShot;
    [SerializeField]
    protected bool reloading = false;
    [SerializeField]
    protected GameObject projectilePrefab;



    protected HUD hud;

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
    protected float MaxRange => maxRange;
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

        float deltaX = mousePos.x - transform.position.x;
        float deltaY = mousePos.y - transform.position.y;

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        Quaternion target = Quaternion.Euler(0, 0, angle);

        transform.rotation = target;
    }

    protected virtual void Fire()
    {
        if (currentAmmo > 0 && !reloading)
        {
            ShootWeapon();
            hud.ReduceAmmoCount(ammoPerShot);
        }
    }

    protected abstract void ShootWeapon();
    protected abstract void SpecialAttack();
    protected virtual void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    protected virtual IEnumerator StartReload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        hud.SetMaxAmmoCount(maxAmmo);
        currentAmmo = maxAmmo;
        reloading = false;
    }
     protected virtual void OnEnable()
     {
        Player.OnShoot += Fire;
        Player.OnSpecial += SpecialAttack;
        Player.OnReload += Reload;
     }
    protected virtual void OnDisable()
    {
        Player.OnShoot -= Fire;
        Player.OnSpecial -= SpecialAttack;
        Player.OnReload -= Reload;
    }
}
