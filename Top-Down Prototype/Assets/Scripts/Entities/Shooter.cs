using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooter : MonoBehaviour, IAttack
{
    #region Fields

    private IShoot gun;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private string targetTag;
    WaitForSeconds timeBetweenShots;
    private float nextTriggerPull;
    private Transform targetTransform;
    private bool weaponFlipped = true;

    #endregion

    #region Properties

    public IShoot CurrentWeapon
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
        gun = GetComponentInChildren<IShoot>();
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        gun.Gun.transform.SetParent(weaponHolder, false);
    }

    private void Update()
    {
        var _direction = targetTransform.position - gun.Gun.transform.position;
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
        gun.Gun.transform.rotation = rotation;
    }

    private void FlipWeapon()
    {
        weaponFlipped = !weaponFlipped;
        Vector3 newScale = weaponHolder.localScale;
        newScale.y *= -1;
        weaponHolder.localScale = newScale;
    }


    #region Public Methods

    public void Attack()
    {
        var direction = targetTransform.position - transform.position;
        gun.Fire(direction);
    }

    public IEnumerator RapidFire()
    {
        if (!gun.Reloading && Time.timeScale > 0)
        {
            if (gun.CanRapidFire)
            {
                while (true && gun.CurrentAmmo > 0)
                {
                    Attack();
                    UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
                    yield return timeBetweenShots;
                }
            }
            else if (Time.time >= nextTriggerPull && gun.CurrentAmmo > 0)
            {
                Attack();
                nextTriggerPull = Time.time + gun.FireRate;
                UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
                yield return null;
            }          
        }
    }

    public void Reload()
    {
        if (!gun.Reloading)
        {
            StartCoroutine(gun.Reload());
        }
    }


    #endregion
}
