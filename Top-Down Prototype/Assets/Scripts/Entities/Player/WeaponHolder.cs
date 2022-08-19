using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHolder : MonoBehaviour
{
    GameObject gunPrefab;
    private List<IShoot> weaponList = new();
    private Pickup[] healthPickups = new Pickup[3];
    private IShoot gun;
    private int maxAmmo;
    public UnityEvent<IShoot> swapEvent;
    public Item ammoPickup;
    public Item healthPickup;
    [SerializeField]
    private IntVariable numPickups;
    public List<IShoot> WeaponList => weaponList;
    public IShoot CurrentWeapon { set => gun = value; }
    public GameObject Gun
    {
        get => gunPrefab;
        set => gunPrefab = value;
    }

    private void Awake()
    {
        gun = GetComponentInChildren<IShoot>();
        gunPrefab = gun.Gun;
        weaponList.Add(gun);
        maxAmmo = gun.MaxAmmo;

    }

    private void Start()
    {
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
    }

    public void TryEquipWeaponOne()
    {
        try
        {
            ChangeWeapon(0);

        }
        catch (System.Exception e)
        {
            gunPrefab.SetActive(true);
            Debug.Log(e);
        }
    }
    public void TryEquipWeaponTwo()
    {
        try
        {
            ChangeWeapon(1);
        }
        catch (System.Exception e)
        {
            gunPrefab.SetActive(true);
            Debug.Log(e);
        }
    }
    public void TryEquipWeaponThree()
    {
        try
        {
            ChangeWeapon(2);
        }
        catch (System.Exception e)
        {
            gunPrefab.SetActive(true);
            Debug.Log(e);
        }
    }

    private void ChangeWeapon(int weaponIndex)
    {
        gunPrefab.SetActive(false);
        gunPrefab = weaponList[weaponIndex].Gun;
        gunPrefab.SetActive(true);
        gun = weaponList[weaponIndex];
        swapEvent?.Invoke(gun);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupObject = collision.gameObject;
        if (pickupObject.TryGetComponent(out IShoot weapon))
        {
            var gunObject = pickupObject.GetComponent<IShoot>() as IShoot;
            GetNewWeapon(gunObject);
        }
        else if (pickupObject.TryGetComponent(out Pickup pickup))
        {
            var consumeable = pickup.Consumable;
            if (consumeable.Contains(ammoPickup))
            {
                AddAmmoToWeapon();
                Destroy(pickupObject);
            }
            else if (consumeable.Contains(healthPickup))
            {
                if (numPickups.Value < healthPickups.Length)
                {
                    healthPickups[numPickups.Value] = pickup;
                    numPickups.Value++;
                    Destroy(pickupObject);
                }
            }
        }
    }

    private void GetNewWeapon(IShoot newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gunPrefab.SetActive(false);
        weaponList.Add(newGun);
        newGun.Gun.transform.SetParent(transform, false);
        newGun.Gun.transform.SetPositionAndRotation(
            gunPrefab.transform.position, Quaternion.identity);
        gunPrefab = newGun.Gun;
        gunPrefab.GetComponent<Collider2D>().enabled = false;
        swapEvent?.Invoke(newGun);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(newGun);
    }

    private void AddAmmoToWeapon()
    {
        AudioManager.Play(AudioClipName.Pickup);
        foreach (IShoot gun in weaponList)
        {
            gun.MaxAmmo += gun.MagazineSize;
            if (gun.Gun.activeInHierarchy)
            {
                UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
            }
        }

    }
}
