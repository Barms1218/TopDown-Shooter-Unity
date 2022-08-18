using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IShoot
{
    [SerializeField] int numProjectiles = 5;
    [SerializeField] GunData data;
    [SerializeField] Transform muzzleTransform;
    private bool firing;
    private int currentAmmo;
    private int maxAmmo;
    private bool reloading = false;
    private WaitForSeconds reloadDelay;
    bool IShoot.CanRapidFire { get => false; }

    public int CurrentAmmo => currentAmmo;

    public int MagazineSize => data.MagazineSize;

    public float FireRate => data.FireRate;

    public int MaxAmmo 
    { 
        get => maxAmmo;
        set => maxAmmo = value; 
    }

    public GameObject Gun => this.gameObject;

    public bool Reloading => reloading;

    void Start()
    {
        reloadDelay = new WaitForSeconds(data.ReloadSpeed);
        maxAmmo = data.StartAmmo;
        currentAmmo = data.MagazineSize;
    }

    public void Fire(Vector2 direction)
    {
        for (int i = 0; i < numProjectiles; i++)
        {
            var bullet = ShotgunPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.SetPositionAndRotation(
                    muzzleTransform.position, muzzleTransform.rotation);
                bullet.SetActive(true);
                bullet.tag = gameObject.tag;
            }

            var projectileScript = bullet.GetComponent<Projectile>();
            direction.y += Random.Range(-data.Recoil, data.Recoil);
            projectileScript.MoveToTarget(direction.normalized);
        }
        currentAmmo--;
        AudioManager.Play(AudioClipName.ShotgunBlast);
        firing = true;
    }

    IEnumerator IShoot.Reload()
    {
        reloading = true;

        while (currentAmmo < data.MagazineSize && !firing && maxAmmo > 0)
        {
            currentAmmo++;
            maxAmmo--;
            UpdateAmmoUI.Instance.UpdateWeaponAmmo(this);
            AudioManager.Play(AudioClipName.ReloadSound);
            yield return reloadDelay;
            reloading = false;
        }
    }
}
