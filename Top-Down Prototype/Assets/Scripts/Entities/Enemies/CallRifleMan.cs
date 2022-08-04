using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRifleMan : MonoBehaviour
{
    [SerializeField] int timeVariation;
    [SerializeField] private float callTimer;
    [SerializeField] private float firstSpawnTime = 15f;
    WaitForSeconds callInterval;
    WaitForSeconds firstSpawnCall;
    private float seconds;

    private void Start()
    {
        callInterval = new WaitForSeconds(callTimer);
        firstSpawnCall = new WaitForSeconds(firstSpawnTime);
        StartCoroutine(Call());
    }

    private IEnumerator Call()
    {
        yield return firstSpawnCall;
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

            yield return callInterval;
        }
    }
}
