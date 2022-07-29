using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float seconds = 0;
    float minutes = 0;
    float timer;
    string timerTime;
    private WaitForSeconds incrementDelay;

    // Start is called before the first frame update
    void Start()
    {
        seconds = Mathf.Floor(timer / 60f);
        minutes = Mathf.Floor(timer / 60f);

        incrementDelay = new WaitForSeconds(1);
        StartCoroutine(IncrementTimer());
    }

    private IEnumerator IncrementTimer()
    {
        while(true)
        {
            timerTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = timerTime;
            seconds++;
            if (seconds == 60)
            {
                minutes++;
            }
            yield return incrementDelay;
        }
    }
}
