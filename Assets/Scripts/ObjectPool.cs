using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SHAREDINSTANCE = null;
    public List<GameObject> pooledObject = null;
    public GameObject objectToPool = null;
    public int amountToPool = 0;

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObject[i].activeInHierarchy) return pooledObject[i];
        }

        return null;
    }
    
    private void Awake()
    {
        SHAREDINSTANCE = this;
    }

    private void Start()
    {
        pooledObject = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            var tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObject.Add(tmp);
        }
    }
    
}
