using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI interactText;

    void Awake()
    {
        interactText.enabled = false;
        Pickup.OnRayCast += SetInteractTextState;
        PlayerWeaponHandler.ReduceAmmo += ReduceAmmoCount;
        PlayerWeaponHandler.SetAmmoCount += ChangeWeaponAmmo;
    }

    // void Update()
    // {
    //     ammoCountText.text = currentAmmo.ToString() + "/" +
    //         maxAmmo.ToString();        
    // }

    public void SetInteractTextState(bool isActive)
    {
        interactText.enabled = isActive;
    }

    private void ReduceAmmoCount(int newAmmo, int currentMaxAmmo)
    {
        ammoCountText.text = newAmmo.ToString() + "/" +
        currentMaxAmmo.ToString(); 
    }

    private void ChangeWeaponAmmo(int startAmmo, int startMaxAmmo)
    {
        ammoCountText.text = startAmmo.ToString() + "/" +
        startMaxAmmo.ToString();         
    }    
}
