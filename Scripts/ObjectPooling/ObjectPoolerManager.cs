using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class ObjectPoolerManager : Singleton<ObjectPoolerManager>
{
    public static ObjectPoolerManager instance;

    private Vector3 defaultPos = new Vector3(-100, -100, -100);

    [SerializeField]
    private List<PoolInfo> listOfPool;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            FillPool(listOfPool[i]);
        }
    }

    private void FillPool(PoolInfo info)
    {
        for (int i = 0; i < info.amount; i++)
        {
            GameObject objInstance = null;
            objInstance = Instantiate(info.prefab, info.container.transform);
            objInstance.SetActive(false);
            objInstance.transform.position = defaultPos;
            info.pool.Add(objInstance);
        }
    }

    public GameObject GetPooledObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;
        GameObject instance = null;
        if (pool.Count > 0)
        {
            instance = pool[pool.Count - 1];
            pool.Remove(instance);
        }
        else
        {
            instance = Instantiate(selected.prefab, selected.container.transform);
        }
        return instance;
    }

    //put object back on the list
    public void CoolObject(GameObject go, PoolObjectType type)
    {
        go.SetActive(false);
        go.transform.position = defaultPos;

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        if (!pool.Contains(go))
        {
            pool.Add(go);
        }
    }

    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        for (int i = 0; i < listOfPool.Count; i++)
        {
            if (type == listOfPool[i].type)
            {
                return listOfPool[i];
            }
        }
        return null;
    }
}
