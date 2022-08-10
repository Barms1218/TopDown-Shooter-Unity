using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableWall : MonoBehaviour, IInteractable
{
    [SerializeField] IHaveHealth health;

    public void Interact()
    {
        health.ChangeHealth(10);
    }
}
