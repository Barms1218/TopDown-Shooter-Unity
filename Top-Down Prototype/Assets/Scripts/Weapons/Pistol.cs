using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IShoot
{
    #region Fields

    [SerializeField] GunData data;
    [SerializeField] Transform muzzleTransform;
    private bool reloading = false;
    private int currentAmmo;
    private int maxAmmo;
    private WaitForSeconds reloadDelay;

    #endregion

    #region Properties
    
    public bool Reloading => reloading;

    public bool CanRapidFire => false;

    public int CurrentAmmo => currentAmmo;

    public int MagazineSize => data.MagazineSize;

    public float FireRate => data.FireRate;

    #endregion

    public int MaxAmmo 
    { 
        get => maxAmmo; 
        set => maxAmmo = value; 
    }

    public GameObject Gun => this.gameObject;

    void Start()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);
        maxAmmo = data.StartAmmo;
        currentAmmo = data.MagazineSize;
    }

    public void Fire(Vector2 direction)
    {
        if (!reloading && currentAmmo > 0)
        {
           var  bullet = PistolPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(
                    muzzleTransform.position, muzzleTransform.rotation);
                bullet.SetActive(true);
                bullet.tag = gameObject.tag;
            }
            var bulletScript = bullet.GetComponent<Projectile>();
            currentAmmo--;
            direction.y += Random.Range(-data.Recoil, data.Recoil);
            bulletScript.MoveToTarget(direction.normalized);
            AudioManager.Play(data.ShootClip);
        }
    }

    IEnumerator IShoot.Reload()
    {
        reloading = true;

        yield return reloadDelay;
        currentAmmo = data.MagazineSize;

        AudioManager.Play(data.ReloadClip);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(this);
        reloading = false;
    }
}
