using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private LayerMask interactLayer;
    public static UnityAction<bool> OnRayCast;
    [SerializeField] private Collider2D playerCollider;

    private void Awake()
    {
        interactLayer = LayerMask.GetMask("Interactables");
    }

    private void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = mousePos - gameObject.transform.position;

        Color lineColor;

        lineColor = Color.red;
        if (Physics2D.Raycast(playerCollider.bounds.center, direction, 2.5f, interactLayer))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                playerCollider.bounds.center, direction, 2.5f, interactLayer);
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (TryGetComponent(out interactable))
            {
                interactable.Interact();
            }
            OnRayCast?.Invoke(true);
            lineColor = Color.green;
        }
        else
        {
            OnRayCast?.Invoke(false);
        }
        Debug.DrawRay(playerCollider.bounds.center, direction, lineColor);
    }
}
