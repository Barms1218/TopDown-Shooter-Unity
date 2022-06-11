using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    int currentAmmo;
    int maxAmmo;

    public void SetMaxAmmoCount(int ammoCount)
    {
        maxAmmo = ammoCount;
        currentAmmo = maxAmmo;
        ammoCountText.text = ammoCount.ToString() + "/" +
            ammoCount.ToString();
    }

    public void ReduceAmmoCount(int reduceAmmount)
    {
        currentAmmo -= reduceAmmount;
        ammoCountText.text = currentAmmo.ToString() + "/" +
            maxAmmo.ToString();
    }
    
    public void AddToAmmoCount(int ammoToAdd)
    {
        currentAmmo += ammoToAdd;
        ammoCountText.text = currentAmmo.ToString() + "/" +
            maxAmmo.ToString();
    }
}
