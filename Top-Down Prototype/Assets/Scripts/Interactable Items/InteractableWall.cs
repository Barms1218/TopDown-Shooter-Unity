using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableWall : MonoBehaviour, IInteractable
{
    [SerializeField] Health health;

    public void Interact()
    {
        health.RestoreHealth(10);
    }
}
