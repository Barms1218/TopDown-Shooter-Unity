using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI interactText;
    int currentAmmo;
    int maxAmmo;

    public int CurrentAmmo
    {
        set { currentAmmo = value; }
        get => currentAmmo;
    }

    public int MaxAmmo
    {
        set { maxAmmo = value; }
        get => maxAmmo;
    }

    void Start()
    {
        interactText.enabled = false;
    }

    void Update()
    {
        ammoCountText.text = currentAmmo.ToString() + "/" +
            maxAmmo.ToString();        
    }

    public void SetInteractTextState(bool isActive)
    {
        interactText.enabled = isActive;
    }

    
}
