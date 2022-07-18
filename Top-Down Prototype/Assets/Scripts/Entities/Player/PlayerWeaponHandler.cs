using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(InputController))]
public class PlayerWeaponHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] protected GameObject gun;
    [SerializeField] private List<GameObject> weaponList = new List<GameObject>();
    public static UnityAction<int, int> ReduceAmmo;
    public static UnityAction<int, int> SetAmmoCount;
    private Weapon currentWeapon;
    private Transform targetTransform;
    private float nextTriggerPull;
    Vector2 _direction;

    #endregion

    #region Properties

    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }
    public List<GameObject> PlayerWeapons => weaponList;
    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    #endregion

    private void Awake()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();
    }

    private void Start()
    {
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    #region Event Methods

    public void Shoot(InputAction.CallbackContext context)
    {
        var fireValue = context.started;
        if (fireValue && currentWeapon.CurrentAmmo > 0 && CanFire)
        {
            currentWeapon.Fire(_direction);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
                        
        }

        if (fireValue && currentWeapon.CurrentAmmo == 0 && CanFire)
        {
            AudioManager.Play(AudioClipName.NoAmmo);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;  
        }
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentWeapon.Reload();
        }
    }

    public void Aim(InputAction.CallbackContext value)
    {
        var aimDirection = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        _direction = aimDirection - transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (currentWeapon != null)
        {
            currentWeapon.Aim(angle, targetTransform);
        }

    }

    #endregion

    public void GetNewWeapon(GameObject newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gun.SetActive(false);
        weaponList.Add(newGun);
        newGun.transform.SetParent(transform);
        newGun.transform.position = gun.transform.position;
        gun = newGun;
        gun.GetComponent<Weapon>().enabled = true;

        // Flip the weapon to proper scale to match player's
        if (newGun.transform.position.x < transform.position.x)
        {
            Vector3 newScale = gun.transform.localScale;
            newScale.x *= -1;
            gun.transform.localScale = newScale;
        }
        currentWeapon = newGun.GetComponent<Weapon>();
        gun.SetActive(true);
        gun.GetComponent<Collider2D>().enabled = false;
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    private void AddAmmoToWeapon(int amountToAdd, int weaponIndex)
    {
        if (weaponList.Contains(weaponList[weaponIndex]))
        {
            weaponList[weaponIndex].GetComponent<Weapon>().MaxAmmo += amountToAdd;
            SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        }
    }

    private bool CanFire => Time.time >= nextTriggerPull;
}
