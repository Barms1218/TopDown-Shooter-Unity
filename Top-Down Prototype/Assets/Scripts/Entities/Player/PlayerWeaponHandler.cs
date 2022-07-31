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
    private Transform targetTransform;
    private List<GameObject> weaponList = new();
    public static UnityAction<int, int> SetAmmoCount;
    private Weapon currentWeapon;
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

    public List<GameObject> PlayerWeapons => weaponList;

    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    public WaitForSeconds TriggerDelay { set { timeBetweenShots = value; } }

    #endregion

    private void Awake()
    {
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();
        targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    private void Start()
    {
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
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
            SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
        }
        else
        {
            AudioManager.Play(AudioClipName.NoAmmo);
        }
    }

    public IEnumerator RapidFire()
    {
        if (currentWeapon.CanRapidFire)
        {
            while(true)
            {
                Shoot();
                yield return timeBetweenShots;
            }
        }
        else if (Time.time >= nextTriggerPull)
        {
            Shoot();
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
            yield return null;
        }
    }


    public void Reload()
    {
        currentWeapon.Reload();
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
        timeBetweenShots = new WaitForSeconds(currentWeapon.TimeBetweenShots);
        gun.SetActive(true);
        currentWeapon.Collider.enabled = false;
        SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
    }

    public void AddAmmoToWeapon(int amountToAdd, string name)
    {
        foreach(GameObject weapon in weaponList)
        {
            if (weapon.name == name)
            {
                weapon.GetComponent<Weapon>().MaxAmmo += amountToAdd;
                SetAmmoCount?.Invoke(currentWeapon.CurrentAmmo, currentWeapon.MaxAmmo);
            }
        }
    }
}
