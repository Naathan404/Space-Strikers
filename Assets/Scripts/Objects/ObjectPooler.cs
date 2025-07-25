using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize;
    private List<GameObject> pooler = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = Instantiate(objectPrefab, transform);
            pooledObject.SetActive(false);
            pooler.Add(pooledObject);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in pooler)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        GameObject pooledObject = Instantiate(objectPrefab, transform);
        pooledObject.SetActive(false);
        pooler.Add(pooledObject);
        return pooledObject;
    }
}
