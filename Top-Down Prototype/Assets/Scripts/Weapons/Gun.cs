using System.Collections; using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Gun : MonoBehaviour
{
    #region Fields

    [SerializeField] protected GunData data;
    [SerializeField] protected Transform muzzleTransform;
    [SerializeField] bool canRapidFire;
    protected GameObject bullet;
    protected bool reloading = false;
    protected WaitForSeconds reloadDelay;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;

    #endregion

    #region Properties

    public int MaxAmmo
    {
        get => maxAmmo;
        set => maxAmmo = value;
    }
    public float FireRate => data.FireRate;
    public int CurrentAmmo => currentAmmo;
    public bool CanRapidFire => canRapidFire;
    public int MagazineSize => data.MagazineSize;

    #endregion


    protected virtual void Start()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);
        maxAmmo = data.StartAmmo;
        currentAmmo = data.MagazineSize;
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

    private void OnEnable()
    {
        //maxAmmo = data.StartAmmo;
        //currentAmmo = data.MagazineSize;
    }
}
