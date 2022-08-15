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


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        callInterval = new WaitForSeconds(callTimer);
        firstSpawnCall = new WaitForSeconds(firstSpawnTime);  
    }

    private IEnumerator Start()
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
            callTimer = Random.Range(callTimer, callTimer + timeVariation);

            yield return callInterval;
        }
    }
}
