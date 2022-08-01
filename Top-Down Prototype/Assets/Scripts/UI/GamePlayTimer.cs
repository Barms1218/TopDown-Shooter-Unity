using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayTimer : MonoBehaviour
{
    private static GamePlayTimer _instance;
    [SerializeField] TextMeshProUGUI timerText;
    //float seconds = 0;
    //float minutes = 0;
    private float timeElapsed;
    string timerTime;
    private WaitForSeconds incrementDelay;

    public TextMeshProUGUI GameTime => timerText;

    public static GamePlayTimer Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("That's not good");
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (PlayerController.player.enabled)
        {
            timeElapsed += Time.deltaTime;
            DisplayTime(timeElapsed);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
