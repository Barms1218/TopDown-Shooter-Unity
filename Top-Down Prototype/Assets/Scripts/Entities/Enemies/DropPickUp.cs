using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUp : MonoBehaviour
{
    [SerializeField] GameObject _ammoDrop;
    [SerializeField] GameObject weaponDrop;
    [SerializeField] private int minChance = 1;
    [SerializeField] private int maxChance = 32;
    [SerializeField] Health health;
    private static bool shotgunDropped = false;
    private static bool rifleDropped = false;

    
    // Start is called before the first frame update
    void Start()
    {
        health.onDiedEvent += DropItem;
    }

    public void DropItem()
    {
        int dropChance = Random.Range(1, 101);
        if (dropChance >= minChance && dropChance <= maxChance)
        {
            var ammo = Instantiate(_ammoDrop, transform.position,
             Quaternion.identity);
        }
        if (weaponDrop.TryGetComponent(out Shotgun shotgun) && !shotgunDropped)
        {
            Instantiate(weaponDrop, transform.position, transform.rotation);
            shotgunDropped = true;
        }
        else if (weaponDrop.TryGetComponent(out AssaultRifle rifle) && !rifleDropped)
        {
            Instantiate(weaponDrop, transform.position, transform.rotation);
            rifleDropped = true;
        }
    }
}
