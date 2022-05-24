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
    protected int weaponDamage;
    [SerializeField]
    protected float reloadSpeed;
    [SerializeField]
    protected float timeBetweenShots;
    [SerializeField]
    protected float maxRange;
    [SerializeField]
    protected bool reloading = false;
    [SerializeField]
    protected GameObject projectilePrefab;

    protected Coroutine fireCoroutine;

    #endregion

    #region Properties

    protected int CurrentAmmo
    {
        get => currentAmmo;
        set => currentAmmo = value;
    }

    protected int MaxAmmo => maxAmmo;
    protected int WeaponDamage => weaponDamage;
    protected float ReloadSpeed => reloadSpeed;
    protected float TimeBetweenShots => timeBetweenShots;
    protected bool Reloading => reloading;

    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    protected abstract void Fire();


    protected abstract void ShootWeapon();

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
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadSpeed);

        currentAmmo = maxAmmo;
        Debug.Log("Weapon Reloaded");

        reloading = false;
    }

    protected abstract void SpecialAttack();

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
