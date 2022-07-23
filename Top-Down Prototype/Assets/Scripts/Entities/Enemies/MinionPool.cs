using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPool : MonoBehaviour
{
    public static MinionPool SharedInstance;
    private List<GameObject> pooledObjects;
    [SerializeField] GameObject minionObject;
    [SerializeField] int amountToPool;


    private void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();


        for (int i = 0; i < amountToPool; i++)
        {
            var minion = Instantiate(minionObject);
            pooledObjects.Add(minion);
            minion.SetActive(false);
        }
    }

    private void Start()
    {

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
