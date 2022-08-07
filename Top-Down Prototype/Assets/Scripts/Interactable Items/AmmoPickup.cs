using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] protected int _amount;
    [SerializeField] protected Gun weaponType;

    public Gun WeaponType => weaponType;
    public int AmountToAdd => _amount;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Play(AudioClipName.Pickup);
        Destroy(gameObject);
    }
}
