using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI interactText;

    private void Awake()
    {
        interactText.enabled = false;
        Pickup.OnRayCast += SetInteractTextState;
        PlayerWeaponHandler.SetAmmoCount += UpdateWeaponAmmo;
    }

    private void SetInteractTextState(bool isActive)
    {
        interactText.enabled = isActive;
    }

    private void UpdateWeaponAmmo(int startAmmo, int startMaxAmmo)
    {
        ammoCountText.text = startAmmo.ToString() + "/" +
        startMaxAmmo.ToString();         
    }    
}
