using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallShotgunZombie : MonoBehaviour
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
            var shotGunner = CaptainPool.SharedInstance.GetPooledObject();
            if (shotGunner != null)
            {
                shotGunner.transform.SetPositionAndRotation(transform.position,
                    transform.rotation);
                shotGunner.SetActive(true);
            }
            callTimer = Random.Range(callTimer - timeVariation,
                callTimer + timeVariation);

            yield return callInterval;
        }
    }
}
