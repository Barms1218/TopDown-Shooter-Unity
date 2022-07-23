using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
    public static BloodPool SharedInstance;
    private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;


    private void Awake()
    {
        SharedInstance = this;    
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject blood;
        for (int i = 0; i < amountToPool; i++)
        {
            blood = Instantiate(objectToPool);
            blood.SetActive(false);
            pooledObjects.Add(blood);
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
