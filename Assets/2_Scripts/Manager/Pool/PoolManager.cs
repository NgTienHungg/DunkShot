using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public PoolObject[] pools;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (PoolObject pool in pools)
        {
            pool.objects = new List<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                pool.objects.Add(CreateGameObject(pool.prefab));
            }
        }
    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        go.SetActive(false);
        return go;
    }

    public GameObject Spawn(ObjectTag tag)
    {
        foreach (PoolObject pool in pools)
        {
            if (pool.objectTag == tag)
            {
                foreach (GameObject go in pool.objects)
                {
                    if (!go.activeInHierarchy)
                    {
                        go.SetActive(true);
                        return go;
                    }
                }
            }

            // expand pool
            if (pool.expandable)
            {
                GameObject go = CreateGameObject(pool.prefab);
                pool.objects.Add(go);
                go.SetActive(true);
                return go;
            }
            else
            {
                Debug.LogWarning("The pool with tag " + tag + " is not expandable, can't spawn more game object!");
                return null;
            }
        }

        Debug.LogWarning("The pool with tag " + tag + " is not exist!");
        return null;
    }
}