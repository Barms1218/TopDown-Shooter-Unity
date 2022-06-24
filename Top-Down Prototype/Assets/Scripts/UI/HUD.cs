using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI interactText;
    int currentAmmo;
    int maxAmmo;
    float timeLeft;

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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        interactText.enabled = false;
        timeLeft = 30f;
        timerText.text = ("Time Left: " + timeLeft.ToString());
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var timer = Mathf.Round(timeLeft - Time.deltaTime);
        timerText.text = ("Time Left: " + timer.ToString());
        ammoCountText.text = currentAmmo.ToString() + "/" +
            maxAmmo.ToString();        
    }

    public void SetInteractTextState(bool isActive)
    {
        Debug.Log(isActive);
        interactText.enabled = isActive;
    }
}
