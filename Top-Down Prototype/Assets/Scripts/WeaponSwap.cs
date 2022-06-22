using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    Player player;

    
    void Awake()
    {
       player  = GetComponent<Player>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // private void Update()
    // {
    //     Swap();
    // }

    public void Swap()
        {
            Vector3 newScale = player.Gun.transform.localScale;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                try
                {
                    
                    player.Gun.SetActive(false);
                    player.Gun = player.WeaponList[0];
                    player.WeaponList[0].SetActive(true);
                    player.CurrentWeapon = player.WeaponList[0].GetComponent<Weapon>();
                }
                catch (System.Exception exception)
                {
                    Debug.Log(exception);
                    player.Gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                try
                {
                    player.Gun.SetActive(false);
                    player.Gun = player.WeaponList[1];
                    player.WeaponList[1].SetActive(true);
                    player.CurrentWeapon = player.WeaponList[1].GetComponent<Weapon>();
                }
                catch(System.Exception exception)
                {
                    Debug.Log(exception);
                    player.Gun.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                try
                {
                    player.Gun.SetActive(false);
                    player.Gun = player.WeaponList[2];
                    player.WeaponList[2].SetActive(true);
                    player.CurrentWeapon = player.WeaponList[2].GetComponent<Weapon>();
                }
                catch(System.Exception exception)
                {
                    Debug.Log(exception);
                    player.Gun.SetActive(true);
                }
            }
        }
}
