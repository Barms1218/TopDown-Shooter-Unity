using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private PlayerWeaponHandler weaponHandler;
    private Weapon weapon;
    private HUD hud;
    private LayerMask interactLayer;
    private RaycastHit2D hit;
    private int gunIndex = 0;
        
    private void Awake()
    {
        weaponHandler = GetComponent<PlayerWeaponHandler>();
        weapon = GetComponent<Weapon>();
        interactLayer = LayerMask.GetMask("Interactables");
        hud = FindObjectOfType<HUD>();
    }

    private void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - gameObject.transform.position;
        
        Color lineColor;
        
        lineColor = Color.red;
        if (Physics2D.Raycast(weaponHandler.GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, 1.5f, interactLayer))
        {
            hit = Physics2D.Raycast(weaponHandler.GetComponent<CapsuleCollider2D>().bounds.center, 
                direction, 1.5f, interactLayer);
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (Input.GetKeyDown(KeyCode.E) && interactable != null)
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interact();
            }
            hud.SetInteractTextState(true);
            lineColor = Color.green;
        } 
        else
        {
            hud.SetInteractTextState(false);
        }     
        Debug.DrawRay(weaponHandler.GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, lineColor);
    }
}
