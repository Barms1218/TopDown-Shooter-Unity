using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    PlayerWeaponHandler weaponHandler;

    void Awake() => weaponHandler = GetComponent<PlayerWeaponHandler>();

    private void Update() => Swap();

    public void Swap()
        {
            Vector3 newScale = weaponHandler.Gun.transform.localScale;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                try
                {
                    weaponHandler.Gun.SetActive(false);
                    weaponHandler.Gun = weaponHandler.WeaponList[0];
                    weaponHandler.WeaponList[0].SetActive(true);
                    weaponHandler.CurrentWeapon = weaponHandler.WeaponList[0].GetComponent<Weapon>();
                }
                catch (System.Exception exception)
                {
                    Debug.Log(exception);
                    weaponHandler.Gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                try
                {
                    weaponHandler.Gun.SetActive(false);
                    weaponHandler.Gun = weaponHandler.WeaponList[1];
                    weaponHandler.WeaponList[1].SetActive(true);
                    weaponHandler.CurrentWeapon = weaponHandler.WeaponList[1].GetComponent<Weapon>();
                }
                catch(System.Exception exception)
                {
                    Debug.Log(exception);
                    weaponHandler.Gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                try
                {
                    weaponHandler.Gun.SetActive(false);
                    weaponHandler.Gun = weaponHandler.WeaponList[2];
                    weaponHandler.WeaponList[2].SetActive(true);
                    weaponHandler.CurrentWeapon = weaponHandler.WeaponList[2].GetComponent<Weapon>();
                }
                catch(System.Exception exception)
                {
                    Debug.Log(exception);
                    weaponHandler.Gun.SetActive(true);
                }
            }
        }
}
