using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    #region Fields

    [SerializeField] private Gun gun;
    [SerializeField] private GameObject weaponHolder;
    private Transform targetTransform;
    Vector2 _direction;
    WaitForSeconds timeBetweenShots;
    private float nextTriggerPull;
    [SerializeField] private bool weaponFlipped = true;

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

    private void Start()
    {
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun.CurrentAmmo, gun.MaxAmmo);
        targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    private void Update()
    {
        _direction = targetTransform.position - gun.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (targetTransform.position.x > transform.position.x && !weaponFlipped)
        {
            FlipWeapon();
        }
        else if (targetTransform.position.x < transform.position.x && weaponFlipped)
        {
            FlipWeapon();
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
        gun.transform.rotation = rotation;
    }

    #region Public Methods

    public void Shoot()
    {
        if (gun.CurrentAmmo > 0)
        {
            gun.Fire(_direction);
            UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun.CurrentAmmo, gun.MaxAmmo);
        }
        else
        {
            AudioManager.Play(AudioClipName.NoAmmo);
        }
    }

    public IEnumerator RapidFire()
    {
        if (gun.CanRapidFire && Time.timeScale > 0)
        {
            while (true && Time.timeScale > 0)
            {
                Shoot();
                yield return timeBetweenShots;
            }
        }
        else if (Time.time >= nextTriggerPull && Time.timeScale > 0)
        {
            Shoot();
            nextTriggerPull = Time.time + gun.FireRate;
            yield return null;
        }
    }

    public void Reload() => gun.Reload();

    #endregion

    private void FlipWeapon()
    {
        weaponFlipped = !weaponFlipped;
        Vector3 newScale = weaponHolder.transform.localScale;
        newScale.y *= -1;
        weaponHolder.transform.localScale = newScale;
    }
}
