using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMinions : MonoBehaviour
{
    [SerializeField] private int timeVariation;
    [SerializeField] private float callTimer;
    WaitForSeconds callDelay;
    


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        callDelay = new WaitForSeconds(callTimer);        
    }

    private IEnumerator Start()
    {
        while(true)
        {
            var minion = MinionPool.SharedInstance.GetPooledObject();
            if (minion != null)
            {
                minion.transform.SetPositionAndRotation(transform.position,
                    transform.rotation);
                minion.SetActive(true);
            }
            callTimer = Random.Range(callTimer, callTimer + timeVariation);
            yield return callDelay;
        }
    }
}
