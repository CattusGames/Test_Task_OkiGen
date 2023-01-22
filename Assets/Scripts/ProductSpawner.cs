using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectList;
    [SerializeField] private float _spawnDelay = 5f;
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int poolSize = 10;
    private float timeSinceLastSpawn;
    private Vector3 spawnPoint;
    private int index = 0;
    private void Start()
    {
        spawnPoint = transform.position;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_objectList[Random.Range(0, _objectList.Count)], Vector3.zero, Quaternion.Euler(new Vector3(Random.Range(0, 180f), Random.Range(0, 180f), Random.Range(0, 180f))));
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= _spawnDelay)
        {
            SpawnObject();
            timeSinceLastSpawn = 0;
        }
    }

    private void SpawnObject()
    {
        if (index == pooledObjects.Count)
        {
            index = 0;
        }
        GameObject obj = GetPooledObject(index);
        obj.transform.position = spawnPoint;
        obj.SetActive(true);
        index++;
    }
    private GameObject GetPooledObject(int index)
    {
        if (!pooledObjects[index].activeInHierarchy)
        {
            return pooledObjects[index];
        }
        else
        {
            GameObject obj = Instantiate(_objectList[Random.Range(0, _objectList.Count)], Vector3.zero, Quaternion.identity);
            pooledObjects.Add(obj);
            return obj;
        }
    }
}


