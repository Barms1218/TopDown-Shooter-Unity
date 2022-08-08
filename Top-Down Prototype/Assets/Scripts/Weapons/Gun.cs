using System.Collections; using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Gun : MonoBehaviour
{
    #region Fields

    [SerializeField] protected GunData data;
    [SerializeField] protected Transform muzzleTransform;
    [SerializeField] Collider2D _collider;
    [SerializeField] bool canRapidFire;
    protected GameObject bullet;
    protected bool reloading = false;
    protected WaitForSeconds reloadDelay;

    #endregion

    #region Properties

    public int MaxAmmo
    {
        get => data.MaxAmmo;
        set => data.MaxAmmo = value;
    }
    public float FireRate => data.FireRate;
    public int CurrentAmmo => data.CurrentAmmo;
    public bool CanRapidFire => canRapidFire;
    public GameObject CurrentGun => data.Gun;

    #endregion


    protected virtual void Awake()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);
    }

    public abstract void Fire(Vector2 direction);

    public virtual void Reload()
    {
        if (!reloading)
        {
            StartCoroutine(StartReload());
        }
    }

    protected abstract IEnumerator StartReload();
}
