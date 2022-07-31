using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int score;


    private void Awake()
    {
        PlayerWeaponHandler.SetAmmoCount += UpdateWeaponAmmo;
        EnemyDeath.GivePoints += UpdatePointsText;
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
