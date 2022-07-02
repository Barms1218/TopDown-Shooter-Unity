using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUp : MonoBehaviour
{
    [SerializeField] GameObject _rifleAmmo;
    [SerializeField] GameObject _shotgunAmmo;
    private int minRifleChance = 1;
    private int maxRifleChance = 12;
    private int minShotGunChance = 33;
    private int maxShotGunChance = 40;
    // Start is called before the first frame update
    void Start()
    {
        
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
