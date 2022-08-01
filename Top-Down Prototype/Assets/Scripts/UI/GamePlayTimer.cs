using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayTimer : MonoBehaviour
{
    private static GamePlayTimer _instance;
    [SerializeField] TextMeshProUGUI timerText;
    float seconds = 0;
    float minutes = 0;
    private float timer;
    string timerTime;
    private WaitForSeconds incrementDelay;

    public TextMeshProUGUI Time => timerText;

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

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        incrementDelay = new WaitForSeconds(timer);
        StartCoroutine(IncrementTimer());
    }

    private IEnumerator IncrementTimer()
    {
        while(PlayerController.player.enabled)
        {
            timerTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = timerTime;
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            yield return incrementDelay;
        }
    }
}
