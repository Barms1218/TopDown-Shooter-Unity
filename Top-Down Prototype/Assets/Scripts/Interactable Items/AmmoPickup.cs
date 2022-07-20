using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AmmoPickup : MonoBehaviour
{
    [SerializeField] protected int _amount;
    [SerializeField] protected string gunName;

    public int Amount => _amount;
    public string GunName => gunName;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Play(AudioClipName.Pickup);
        Destroy(gameObject);
    }
}
