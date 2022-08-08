using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunZombiePool : MonoBehaviour
{
    private static ShotgunZombiePool _instance;
    private List<GameObject> pooledObjects;
    [SerializeField] GameObject minionObject;
    [SerializeField] int amountToPool;

    public static ShotgunZombiePool SharedInstance
    {
        get
        {
            if (_instance == null)
                Debug.Log("I can't do that");
            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
        pooledObjects = new List<GameObject>();


        for (int i = 0; i < amountToPool; i++)
        {
            var minion = Instantiate(minionObject);
            pooledObjects.Add(minion);
            minion.SetActive(false);
        }
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
