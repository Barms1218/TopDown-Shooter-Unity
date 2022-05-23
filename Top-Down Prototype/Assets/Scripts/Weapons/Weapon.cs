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
    protected float timeBetweenShots;
    [SerializeField]
    protected float reloadSpeed;
    [SerializeField]
    protected bool reloading = false;

    protected Coroutine fireCoroutine;

    #endregion

    #region Properties

    public int CurrentAmmo
    {
        get => currentAmmo;
        set => currentAmmo = value;
    }

    public int MaxAmmo => maxAmmo;
    public int WeaponDamage => weaponDamage;
    public float TimeBetweenShots => timeBetweenShots;
    public float ReloadSpeed => reloadSpeed;
    public bool Reloading => reloading;

    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot()
    {
        if (currentAmmo > 0)
        {
            if (Input.GetMouseButton(0))
            {
                fireCoroutine = StartCoroutine(FireCoroutine());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopCoroutine(FireCoroutine());
            }
        }
    }


    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Debug.Log("Shot Weapon");
            currentAmmo--;

            yield return new WaitForSeconds(TimeBetweenShots);
        }

    }

    public virtual void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(ReloadDelay());
        }
    }

    IEnumerator ReloadDelay()
    {
        reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadSpeed);

        currentAmmo = maxAmmo;
        Debug.Log("Weapon Reloaded");

        reloading = false;
    }

    public abstract void SpecialAttack();


    protected virtual void OnEnable()
    {
        PlayerShoot.OnShoot += Shoot;
        PlayerShoot.OnReload += Reload;
    }
    protected virtual void OnDisable()
    {
        PlayerShoot.OnShoot -= Shoot;
        PlayerShoot.OnReload -= Reload;
    }
}
