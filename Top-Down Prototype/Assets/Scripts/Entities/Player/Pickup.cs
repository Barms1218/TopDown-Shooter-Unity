using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    public static UnityAction<string> AddWeaponName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            AddWeaponName?.Invoke(collision.gameObject.name);
            collision.gameObject.GetComponent<IInteractable>().Interact();
        }
    }
}
