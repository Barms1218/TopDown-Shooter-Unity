using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWeaponHandler : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private PlayerController _controller;
    private Weapon currentWeapon;
    private Transform targetTransform;
    Vector2 _direction;
    WaitForSeconds timeBetweenShots;
    private float nextTriggerPull;
    private float triggerTime;

    #endregion

    #region Properties

    public Weapon CurrentWeapon
    {
        set => currentWeapon = value;
    }

    public float TriggerDelay
    {
        set
        {
            triggerTime = value;
            timeBetweenShots = new WaitForSeconds(triggerTime);
        }
    }

    #endregion

    private void Start()
    {
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    private void Update()
    {
        Aim();
    }

    #region Public Methods

    public void Aim()
    {
        _direction = targetTransform.position - currentWeapon.gameObject.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (currentWeapon != null)
        {
            currentWeapon.Aim(angle, targetTransform.position);
        }
    }

    private void FireWeapon()
    {
        if (currentWeapon.CurrentAmmo > 0)
        {
            currentWeapon.Fire(_direction);
            UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        }
        else
        {
            AudioManager.Play(AudioClipName.NoAmmo);
        }
    }

    public IEnumerator RapidFire()
    {
        if (currentWeapon.CanRapidFire && Time.timeScale > 0)
        {
            while(true)
            {
                FireWeapon();
                yield return timeBetweenShots;
            }
        }
        else if (Time.time >= nextTriggerPull && Time.timeScale > 0)
        {
            FireWeapon();
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
            yield return null;
        }
    }

    public void Reload() => currentWeapon.Reload();

    #endregion

    //public void AddAmmoToWeapon(int amountToAdd, Weapon weapon)
    //{
    //    if (weaponList.Contains(weapon))
    //    {
    //        weapon.MaxAmmo += amountToAdd;
    //    }
    //}
}
