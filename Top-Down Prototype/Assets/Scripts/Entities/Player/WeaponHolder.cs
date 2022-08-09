using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponHolder : MonoBehaviour
{
    GameObject gunPrefab;
    private List<Gun> weaponList = new();
    private Gun gun;
    public UnityEvent<Gun> swapEvent;

    public List<Gun> WeaponList => weaponList;
    public Gun CurrentWeapon { set => gun = value; }
    public GameObject Gun
    {
        get => gunPrefab;
        set => gunPrefab = value;
    }

    private void Awake()
    {
        gun = GetComponentInChildren<Gun>();
        gunPrefab = gun.gameObject;
        weaponList.Add(gun);
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

    void ChangeWeapon(int weaponIndex)
    {
        gunPrefab.SetActive(false);
        gunPrefab = weaponList[weaponIndex].gameObject;
        gunPrefab.SetActive(true);
        gun = weaponList[weaponIndex];
        swapEvent?.Invoke(gun);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(gun);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupObject = collision.gameObject;
        if (pickupObject.TryGetComponent(out Gun weapon))
        {
            GetNewWeapon(weapon);
        }
    }

    public void GetNewWeapon(Gun newGun)
    {
        AudioManager.Play(AudioClipName.GetGun);
        gunPrefab.SetActive(false);
        weaponList.Add(newGun);
        newGun.transform.SetParent(transform, false);
        newGun.transform.SetPositionAndRotation(gunPrefab.transform.position, Quaternion.identity);
        gunPrefab = newGun.gameObject;

        swapEvent?.Invoke(newGun);
        UpdateAmmoUI.Instance.UpdateWeaponAmmo(newGun);
    }

    public void AddAmmoToWeapon(int amountToAdd, Gun weapon)
    {
        if (weaponList.Contains(weapon))
        {
            weapon.MaxAmmo += amountToAdd;
        }
    }
}
