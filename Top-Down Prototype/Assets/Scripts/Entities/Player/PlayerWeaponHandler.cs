using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[RequireComponent(typeof(InputController))]
public class PlayerWeaponHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject gun;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private List<Weapon> weaponList = new List<Weapon>();
    [SerializeField] private Weapon currentWeapon;
    private Transform targetTransform;
    Vector2 _direction;
    WaitForSeconds timeBetweenShots;
    private float nextTriggerPull;

    #endregion

    #region Properties

    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }

    public List<Weapon> PlayerWeapons => weaponList;

    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    public WaitForSeconds TriggerDelay { set { timeBetweenShots = value; } }

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
        _direction = targetTransform.position - gun.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (currentWeapon != null)
        {
            currentWeapon.Aim(angle, targetTransform.position);
        }
    }

    private void Shoot()
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
                Shoot();
                yield return timeBetweenShots;
            }
        }
        else if (Time.time >= nextTriggerPull && Time.timeScale > 0)
        {
            Shoot();
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
            yield return null;
        }
    }

    public void Reload() => currentWeapon.Reload();

    #endregion

    public void GetNewWeapon(Weapon newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gun.SetActive(false);
        weaponList.Add(newGun);
        newGun.transform.SetParent(transform, false);
        newGun.transform.position = gun.transform.position;
        gun = newGun.gameObject;

        currentWeapon = newGun;
        timeBetweenShots = new WaitForSeconds(currentWeapon.TimeBetweenShots);
        currentWeapon.Collider.enabled = false;
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    public void AddAmmoToWeapon(int amountToAdd, string name)
    {
        foreach(Weapon weapon in weaponList)
        {
            if (weapon.name == name)
            {
                weapon.GetComponent<Weapon>().MaxAmmo += amountToAdd;
                UpdateAmmoUI.Instance.UpdateWeaponAmmo(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
            }
        }
    }
}
