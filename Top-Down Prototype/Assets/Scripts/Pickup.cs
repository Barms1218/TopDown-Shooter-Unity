using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Player player;
    LayerMask weaponLayer;  
 
    int gunIndex = 0;    
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        weaponLayer = LayerMask.GetMask("Weapons");
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        PickUpGun();
    }
    private void PickUpGun()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - gameObject.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(player.GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, 1.5f, weaponLayer);
        Color lineColor;
        
        lineColor = Color.red;
        if (hit.collider != null)
        {
            Debug.Log("Press E to pick up");
            if (Input.GetKeyDown(KeyCode.E))
            {
                gunIndex++;
                player.WeaponList.Add(hit.collider.gameObject);
                player.WeaponList[gunIndex].transform.SetParent(player.transform);
                player.WeaponList[gunIndex].transform.position = player.Gun.transform.position;

                player.Gun.SetActive(false);
                player.Gun = player.WeaponList[gunIndex];
                if (player.Gun.transform.position.x < transform.position.x)
                {
                    Vector3 newScale = player.Gun.transform.localScale;
                    newScale.x *= -1;
                    player.Gun.transform.localScale = newScale;
                }
                player.WeaponList[gunIndex].GetComponent<Weapon>().enabled = true;
                player.CurrentWeapon = player.WeaponList[gunIndex].GetComponent<Weapon>();
                hit.collider.enabled = false;
            }
            else
            {
                lineColor = Color.green;
            }

            
        }
        Debug.DrawRay(player.GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, lineColor);
    }
}
