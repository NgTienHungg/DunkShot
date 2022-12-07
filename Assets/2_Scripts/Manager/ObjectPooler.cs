using System;
using UnityEngine;
using System.Collections.Generic;

public enum ObjectTag
{
    Ball = 0,
    Basket,
    TrajectoryDot,
    Star
}

[Serializable]
public class Pool
{
    public GameObject prefab;
    public ObjectTag objectTag;
    public int poolSize;
    public bool expandable;

    [HideInInspector]
    public List<GameObject> listObject;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    public Pool[] pools;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var pool in pools)
        {
            pool.listObject = new List<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                pool.listObject.Add(CreateGameObject(pool.prefab));
            }
        }
    }

    public GameObject GetPrefab(ObjectTag tag)
    {
        GameObject prefab = null;
        foreach (Pool pool in pools)
        {
            if (pool.objectTag == tag)
            {
                prefab = pool.prefab;
                break;
            }
        }
        return prefab;
    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        return obj;
    }

    public GameObject Spawn(ObjectTag tag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.objectTag == tag)
            {
                foreach (GameObject go in pool.listObject)
                {
                    if (!go.activeInHierarchy)
                    {
                        go.SetActive(true);
                        return go;
                    }
                }

                // expand pool
                if (pool.expandable)
                {
                    GameObject obj = CreateGameObject(pool.prefab);
                    pool.listObject.Add(obj);
                    obj.SetActive(true);
                    return obj;
                }
                else
                {
                    Debug.LogWarning("The pool with tag " + tag + " is not expandable, can't spawn more game object!");
                    return null;
                }
            }
        }

        Debug.LogWarning("The pool with tag " + tag + " is not exist!");
        return null;
    }

    public GameObject Spawn(ObjectTag tag, Transform parent)
    {
        GameObject obj = Spawn(tag);
        obj.transform.parent = parent;
        return obj;
    }

    public GameObject Spawn(ObjectTag tag, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Spawn(tag);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public void Recall(GameObject obj)
    {
        MonoBehaviour instance = obj.GetComponent<MonoBehaviour>();
        if (instance is Ball)
        {
            (instance as Ball).Renew();
        }
        else if (instance is Basket)
        {
            (instance as Basket).Renew();
        }

        obj.transform.parent = transform;
        obj.SetActive(false);
    }

    public void RecallAll()
    {
        Debug.Log("Object Pooler recall all");
        foreach (var pool in pools)
        {
            // vì không làm reload scene nên không c?n thu h?i TrajectoryDot
            if (pool.objectTag == ObjectTag.TrajectoryDot)
                return;

            foreach (var obj in pool.listObject)
            {
                Recall(obj);
            }
        }
    }
}