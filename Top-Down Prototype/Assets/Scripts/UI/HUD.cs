using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] IntVariable scoreObject;

    [SerializeField] TextMeshProUGUI killText;
    [SerializeField] IntVariable killObject;

    private int score;
    private int killCount;

    public int Score => score;

    private void Update()
    {
        score = scoreObject.Value;
        scoreText.text = "Score: " + score.ToString();

        killCount = killObject.Value;
        killText.text = "Targets: " + killCount.ToString();
    }
}
