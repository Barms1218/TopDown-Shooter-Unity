using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD _instance;
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int score;

    public static HUD Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Dude, there's no HUD.");
            }
            return _instance;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

    }

    private void Awake()
    {
        _instance = this;

        score = 0;
    }

    public void UpdateWeaponAmmo(int startAmmo, int startMaxAmmo)
    {
        ammoCountText.text = startAmmo.ToString() + "/" +
        startMaxAmmo.ToString();         
    }

    public void UpdatePointsText(int points)
    {
        score += points;
        scoreText.text = ("Score: " + score.ToString());
    }
}
