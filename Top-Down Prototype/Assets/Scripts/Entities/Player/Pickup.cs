using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    private LayerMask interactLayer;
    public static UnityAction<bool> OnRayCast;

    private void Awake() => interactLayer = LayerMask.GetMask("Interactables");

    private void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - gameObject.transform.position;
        
        Color lineColor;
        
        lineColor = Color.red;
        if (Physics2D.Raycast(GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, 1.5f, interactLayer))
        {
            RaycastHit2D hit = Physics2D.Raycast(GetComponent<CapsuleCollider2D>().bounds.center, 
                direction, 2.5f, interactLayer);
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (Input.GetKeyDown(KeyCode.E) && interactable != null)
            {
                hit.collider.gameObject.GetComponent<IInteractable>().Interact();
            }
            OnRayCast?.Invoke(true);
            lineColor = Color.green;
        } 
        else
        {
            OnRayCast?.Invoke(false);
        }     
        Debug.DrawRay(GetComponent<CapsuleCollider2D>().bounds.center, 
        direction, lineColor);
    }
}
