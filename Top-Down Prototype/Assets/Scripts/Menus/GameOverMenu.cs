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
    [SerializeField] Canvas canvas;

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
        DontDestroyOnLoad(this);
    }

    public void ShowGameOverScreen()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        killCountText.text = "Enemies Killed: " + GamePlayManager.Instance.KillCount.ToString();
        finalScoreText.text = "Final Score: " + HUD.Instance.Score;
        finalTimeText.text = "You Survived For: " + GamePlayTimer.Instance.GameTime.text;
    }
}
