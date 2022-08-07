using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    #region Fields

    private Gun weapon;
    private Transform targetTransform;
    Vector2 _direction;
    WaitForSeconds timeBetweenShots;
    private float nextTriggerPull;
    private bool weaponFlipped = false;

    #endregion

    #region Properties

    public Gun CurrentWeapon
    {
        set
        {
            weapon = value;
            timeBetweenShots = new WaitForSeconds(weapon.FireRate);
        }
    }

    #endregion

    private void Awake()
    {
        weapon.gameObject.transform.SetParent(transform, false);
    }

    private void Start()
    {
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(weapon.CurrentAmmo, weapon.MaxAmmo);
        targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    private void Update()
    {
        _direction = targetTransform.position - weapon.gameObject.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (targetTransform.position.x > transform.position.x && weaponFlipped)
        {
            FlipWeapon();
        }
        else if (targetTransform.position.x < transform.position.x && !weaponFlipped)
        {
            FlipWeapon();
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
        weapon.transform.rotation = rotation;
    }

    #region Public Methods

    private void Attack()
    {
        if (weapon.CurrentAmmo > 0)
        {
            weapon.Fire(_direction);
            UpdateAmmoUI.Instance.UpdateWeaponAmmo(weapon.CurrentAmmo, weapon.MaxAmmo);
        }
        else
        {
            AudioManager.Play(AudioClipName.NoAmmo);
        }
    }

    public IEnumerator RapidFire()
    {
        if (weapon.CanRapidFire && Time.timeScale > 0)
        {
            while(true)
            {
                Attack();
                yield return timeBetweenShots;
            }
        }
        else if (Time.time >= nextTriggerPull && Time.timeScale > 0)
        {
            Attack();
            nextTriggerPull = Time.time + weapon.FireRate;
            yield return null;
        }
    }

    public void Reload() => weapon.Reload();

    private void FlipWeapon()
    {
        weaponFlipped = !weaponFlipped;
        Vector3 newScale = weapon.transform.localScale;
        newScale.y *= -1;
        //newScale.x *= -1;
        weapon.transform.localScale = newScale;
    }

    #endregion

}
