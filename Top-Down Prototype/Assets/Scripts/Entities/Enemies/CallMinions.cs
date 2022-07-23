using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMinions : MonoBehaviour
{
    [SerializeField] float callTimer;
    WaitForSeconds callDelay;
    

    private void Start()
    {
        callDelay = new WaitForSeconds(callTimer);
        StartCoroutine(Call());
    }

    private IEnumerator Call()
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

            yield return callDelay;
        }
    }
}
