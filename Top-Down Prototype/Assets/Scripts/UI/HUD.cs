using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    int currentAmmo;
    int maxAmmo;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
}
