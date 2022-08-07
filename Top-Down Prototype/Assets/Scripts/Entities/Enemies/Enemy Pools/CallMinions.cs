using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMinions : MonoBehaviour
{
    [SerializeField] private int timeVariation;
    [SerializeField] private float callTimer;
    [SerializeField] private float minionSpeed;
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
                if (minion.TryGetComponent(out Move move))
                {
                    move.Speed = minionSpeed;
                }
            }
            callTimer = Random.Range(callTimer - timeVariation,
                callTimer + timeVariation);
            yield return callDelay;
        }
    }
}
