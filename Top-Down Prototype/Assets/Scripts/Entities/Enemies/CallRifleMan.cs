using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRifleMan : MonoBehaviour
{
    [SerializeField] int timeVariation;
    [SerializeField] private float callTimer;
    WaitForSeconds callDelay;
    private float seconds;

    private void Start()
    {
        callDelay = new WaitForSeconds(callTimer);
        GamePlayTimer.onTimeElapsed += BeginCall;
        StartCoroutine(Call());
    }

    private void BeginCall()
    {
        StartCoroutine(Call());
    }

    private IEnumerator Call()
    {
        while (true)
        {
            if (seconds < 30)
            {
                seconds++;
                Debug.Log(callDelay);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                var rifleMan = RifleManPool.SharedInstance.GetPooledObject();
                if (rifleMan != null)
                {
                    rifleMan.transform.SetPositionAndRotation(transform.position,
                        transform.rotation);
                    rifleMan.SetActive(true);
                }
                callTimer = Random.Range(callTimer - timeVariation,
                    callTimer + timeVariation);

                yield return callDelay;
            }
        }
    }
}
