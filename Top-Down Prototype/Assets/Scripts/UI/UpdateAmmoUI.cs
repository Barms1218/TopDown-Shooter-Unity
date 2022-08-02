using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateAmmoUI : MonoBehaviour
{
    private static UpdateAmmoUI _instance;
    [SerializeField] private TextMeshProUGUI ammoText;

    public static UpdateAmmoUI Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("There's no way");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateWeaponAmmo(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }
}
