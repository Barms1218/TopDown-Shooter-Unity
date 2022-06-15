using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] 
    TextMeshProUGUI ammoCountText;
    [SerializeField] 
    TextMeshProUGUI timerText;
    [SerializeField] 
    TextMeshProUGUI scoreText;
    int currentAmmo;
    int maxAmmo;
    float timeLeft;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        timeLeft = 30f;
        timerText.text = ("Time Left: " + timeLeft.ToString());
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        float timer = Mathf.Round(timeLeft - Time.deltaTime);
        timerText.text = ("Time Left: " + timer.ToString());
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
    
    public void AddToAmmoCount(int ammoToAdd)
    {
        currentAmmo += ammoToAdd;
        ammoCountText.text = currentAmmo.ToString() + "/" +
            maxAmmo.ToString();
    }
}
