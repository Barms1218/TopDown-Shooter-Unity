using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class WeaponSwap : MonoBehaviour
{
    [SerializeField] private List<string> weapons = new List<string>();
    public static UnityAction<string> OnWeaponSwap;

    private void Awake() => Pickup.AddWeaponName += AddWeapon;
    private void Update()
    {
        // Initial weapon in player's inventory should always be pistol
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            try
            {
                OnWeaponSwap?.Invoke("Pistol");
            }
            catch (System.Exception exception)
            {
                Debug.Log(exception);               
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            try
            {
                OnWeaponSwap?.Invoke(weapons[0]);
            }
            catch(System.Exception exception)
            {
                Debug.Log(exception);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            try
            {
                OnWeaponSwap?.Invoke(weapons[1]);
            }
            catch(System.Exception exception)
            {
                Debug.Log(exception);
            }
        }        
    }

    private void AddWeapon(string weaponName)
    {
        weapons.Add(weaponName);
    }
}
