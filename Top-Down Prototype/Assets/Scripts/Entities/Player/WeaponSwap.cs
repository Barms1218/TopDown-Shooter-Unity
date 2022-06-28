using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerWeaponHandler))]
public class WeaponSwap : MonoBehaviour
{
    public static UnityAction<int> OnWeaponSwap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            try
            {
                OnWeaponSwap?.Invoke(0);
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
                OnWeaponSwap?.Invoke(1);
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
                OnWeaponSwap?.Invoke(2);
            }
            catch(System.Exception exception)
            {
                Debug.Log(exception);
            }
        }        
    }
}
