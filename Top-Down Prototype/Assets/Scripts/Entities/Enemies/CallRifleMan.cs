using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRifleMan : MonoBehaviour
{
    [SerializeField] float timeVariation;
    [SerializeField] private float callTimer;
    WaitForSeconds callDelay;


    private void Start()
    {
        callDelay = new WaitForSeconds(callTimer);
        //StartCoroutine(Call());
    }

    private void Update()
    {
        if (Time.time == 30f)
        {
            Debug.Log("Bringing out the snipey bois");
            StartCoroutine(Call());
        }
    }

    private IEnumerator Call()
    {
        while (true)
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
            Debug.Log(callDelay);
            yield return callDelay;
        }
    }
}
