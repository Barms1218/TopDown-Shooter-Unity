using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUp : MonoBehaviour
{
    [SerializeField] GameObject _ammoDrop;
    [SerializeField] private int minChance = 1;
    [SerializeField] private int maxChance = 32;

    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddHealthListener(DropAmmo);
    }

    public void DropAmmo(GameObject dropper)
    {
        int dropChance = Random.Range(1, 101);
        if (dropChance >= minChance && dropChance <= maxChance)
        {
            var rifleAmmoDrop = Instantiate(_ammoDrop, transform.position,
             Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(DropAmmo);
    }
}
