using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooter : MonoBehaviour, IAttack
{
    #region Fields

    private Gun gun;
    WaitForSeconds timeBetweenShots;
    private float nextTriggerPull;

    #endregion

    #region Properties

    public Gun CurrentWeapon
    {
        set
        {
            gun = value;
            timeBetweenShots = new WaitForSeconds(gun.FireRate);
        }
    }

    #endregion

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
    }

    #region Public Methods

    public void Attack()
    {
        gun.Fire(Vector2.right);
    }

    public IEnumerator RapidFire()
    {
        if (gun.CanRapidFire && gun.CurrentAmmo > 0 && Time.timeScale > 0)
        {
            while (true && Time.timeScale > 0)
            {
                Attack();
                UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
                yield return timeBetweenShots;
            }
        }
        else if (Time.time >= nextTriggerPull && gun.CurrentAmmo > 0 && Time.timeScale > 0)
        {
            Attack();
            nextTriggerPull = Time.time + gun.FireRate;
            UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
            yield return null;
        }
        else if (gun.CurrentAmmo <= 0)
        {
            AudioManager.Play(AudioClipName.NoAmmo);
        }
    }

    public void Reload() => gun.Reload();

    #endregion
}
