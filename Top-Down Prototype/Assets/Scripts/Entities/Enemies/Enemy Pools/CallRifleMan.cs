using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRifleman : MonoBehaviour
{
    [SerializeField] int timeVariation;
    [SerializeField] private float callTimer;
    [SerializeField] private float firstSpawnTime = 15f;
    WaitForSeconds callInterval;
    WaitForSeconds firstSpawnCall;

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
            var rifleman = RifleManPool.SharedInstance.GetPooledObject();
            if (rifleman != null)
            {
                rifleman.transform.SetPositionAndRotation(transform.position,
                    transform.rotation);
                rifleman.SetActive(true);
            }
            callTimer = Random.Range(callTimer - timeVariation,
                callTimer + timeVariation);

            yield return callInterval;
        }
    }
}