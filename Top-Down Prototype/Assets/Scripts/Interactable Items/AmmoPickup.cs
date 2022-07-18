using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AmmoPickup : MonoBehaviour
{
    [SerializeField] int _amount;
    protected string gunName;
    public static UnityAction<int, ammoTypes> OnTrigger;

    public enum ammoTypes
    {
        Rifle,
        Shotgun
    }

    protected ammoTypes ammoType;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnTrigger?.Invoke(_amount, ammoType);
            AudioManager.Play(AudioClipName.Pickup);
            Destroy(gameObject);
        }
    }
}
