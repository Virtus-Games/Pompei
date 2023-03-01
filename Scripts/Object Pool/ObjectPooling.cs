using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pool
{
    public Queue<GameObject> PoolObjects;
    public GameObject ObjectPrefab;
    public int poolLength;
}

public class ObjectPooling : Singleton<ObjectPooling>
{

    public Pool[] Pools;

    private void Awake()
    {
        for (int i = 0; i < Pools.Length; i++)
        {
            Pools[i].PoolObjects = new Queue<GameObject>();
            AddObject(Pools[i].poolLength, i);
        }
    }


    public GameObject GetObjectPooling(int number)
    {
        if (number >= Pools.Length) return null;
        if (Pools[number].PoolObjects.Count == 0) AddObject(5, number);

        GameObject obj = Pools[number].PoolObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void SetObjectPool(GameObject obj, int number)
    {
        if (number >= Pools.Length) return;
        Pools[number].PoolObjects.Enqueue(obj);
        obj.SetActive(false);

    }

    public void AddObject(int amount, int number)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(Pools[number].ObjectPrefab);
            obj.SetActive(false);
            Pools[number].PoolObjects.Enqueue(obj);
        }
    }
}
