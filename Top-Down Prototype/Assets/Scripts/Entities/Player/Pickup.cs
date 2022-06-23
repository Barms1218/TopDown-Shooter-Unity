using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    PlayerWeaponHandler weaponHandler;
    Weapon weapon;
    LayerMask weaponLayer;  
 
    int gunIndex = 0;
        
    // Start is called before the first frame update
    void Awake()
    {
        weaponHandler = GetComponent<PlayerWeaponHandler>();
        weapon = GetComponent<Weapon>();
        weaponLayer = LayerMask.GetMask("Weapons");
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update() => PickUpGun();
    
    private void PickUpGun()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - gameObject.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(weaponHandler.GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, 1.5f, weaponLayer);
        Color lineColor;
        
        lineColor = Color.red;
        if (hit.collider != null)
        {
            Debug.Log("Press E to pick up");
            if (Input.GetKeyDown(KeyCode.E))
            {
                gunIndex++;
                weaponHandler.WeaponList.Add(hit.collider.gameObject);
                weaponHandler.WeaponList[gunIndex].transform.SetParent(weaponHandler.transform);
                weaponHandler.WeaponList[gunIndex].transform.position = weaponHandler.Gun.transform.position;

                weaponHandler.Gun.SetActive(false);
                weaponHandler.Gun = weaponHandler.WeaponList[gunIndex];
                if (weaponHandler.Gun.transform.position.x < transform.position.x)
                {
                    Vector3 newScale = weaponHandler.Gun.transform.localScale;
                    newScale.x *= -1;
                    weaponHandler.Gun.transform.localScale = newScale;
                }
                weaponHandler.WeaponList[gunIndex].GetComponent<Weapon>().enabled = true;
                weaponHandler.CurrentWeapon = weaponHandler.WeaponList[gunIndex].GetComponent<Weapon>();
                hit.collider.enabled = false;
            }
            else
            {
                lineColor = Color.green;
            }

            
        }
        Debug.DrawRay(weaponHandler.GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, lineColor);
    }
}
