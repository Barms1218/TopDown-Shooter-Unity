using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum PickupType
{
    Ammo,
    Health
}

public class Pickup : MonoBehaviour
{
    public PickupType pickupType;
}
