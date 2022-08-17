using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IShoot
{
    #region Fields
    [SerializeField] GunData data;
    [SerializeField] Transform muzzleTransform;
    private bool firing;
    private int currentAmmo;
    private int maxAmmo;
    private bool reloading = false;
    private WaitForSeconds reloadDelay;

    #endregion

    #region Properties

    public bool Reloading => reloading;
    public bool CanRapidFire => true;

    public int CurrentAmmo => currentAmmo;

    public int MagazineSize => data.MagazineSize;

    public int MaxAmmo 
    {
         get => maxAmmo; 
         set => maxAmmo = value; 
    }

    public float FireRate => data.FireRate;

    public GameObject Gun => this.gameObject;

    #endregion

    void Start()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);
        maxAmmo = data.StartAmmo;
        currentAmmo = data.MagazineSize;
    }

    public void Fire(Vector2 direction)
    {
        if (!reloading)
        {
            var bullet = RiflePool.SharedInstance.GetPooledObject();
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
            AudioManager.Play(AudioClipName.AR_Fire);
        }
    }

    IEnumerator IShoot.StartReload()
    {
        reloading = true;
        yield return reloadDelay;
        if (maxAmmo > data.MagazineSize - currentAmmo)
        {
            maxAmmo -= data.MagazineSize - currentAmmo;
            currentAmmo = data.MagazineSize;
        }
        else if (maxAmmo < data.MagazineSize - currentAmmo)
        {
            currentAmmo += maxAmmo;
            maxAmmo = 0;
        }

        UpdateAmmoUI.Instance.UpdateWeaponAmmo(this);      
        reloading = false;
        AudioManager.Play(AudioClipName.ReloadSound);
    }
}
