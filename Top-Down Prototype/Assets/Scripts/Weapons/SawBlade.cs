using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Weapon
{
    [SerializeField] GunData gunData;
    float timeSinceLastShot;
    new void Start()
    {

    }

    public override void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Shot Weapon");
                gunData.currentAmmo--;
            }
        }
    }

    public override void Reload()
    {
        if (!gunData.reloading)
        {
            ReloadWeapon();
            Debug.Log("Reloading");
        }
    }

    IEnumerator ReloadWeapon()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadSpeed);
        gunData.currentAmmo = gunData.maxAmmo;
        Debug.Log("Weapon Reloaded");

        gunData.reloading = false;
    }

    private void OnEnable()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += Reload;
    }
    private void OnDisable()
    {
        PlayerShoot.shootInput -= Shoot;
        PlayerShoot.reloadInput -= Reload;
    }
}
