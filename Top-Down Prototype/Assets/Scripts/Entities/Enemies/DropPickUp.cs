using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUp : MonoBehaviour
{
    [SerializeField] GameObject _rifleAmmo;
    [SerializeField] GameObject _shotgunAmmo;
    [SerializeField] private int minRifleChance = 1;
    [SerializeField] private int maxRifleChance = 12;
    [SerializeField] private int minShotGunChance = 33;
    [SerializeField] private int maxShotGunChance = 40;
    
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Health>().OnDied += DropAmmo;
    }

    public void DropAmmo()
    {
        int dropChance = Random.Range(1, 101);
        if (dropChance >= minRifleChance && dropChance <= maxRifleChance)
        {
            var rifleAmmoDrop = Instantiate(_rifleAmmo, transform.position,
             Quaternion.identity);
        }
        else if (dropChance >= minShotGunChance && dropChance <= maxShotGunChance)
        {
            var shotgunAmmoDrop = Instantiate(_shotgunAmmo, transform.position,
             Quaternion.identity);
        }
    }
}
