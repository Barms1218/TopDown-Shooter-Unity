using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    private static GameOverMenu _instance;
    [SerializeField] TextMeshProUGUI killCountText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI finalTimeText;

    private int killCount;
    private int score;

    public static GameOverMenu Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("There is no Game Over Menu!");
            }
            return _instance;
        }
    }

    public int KillCount
    {
        get
        {
            return killCount;
        }
    }

    private void Awake()
    {
        _instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        finalTimeText.text = "You Survived For: " + GamePlayTimer.Instance.Time.text;
    }

    public void UpdateKillCount()
    {
        killCount++;
        killCountText.text = "Enemies Killed: " + killCount.ToString();
    }

    public void UpdateFinalScore(int points)
    {
        score += points;
        finalScoreText.text = "Final Score:" + score.ToString();
    }
}
