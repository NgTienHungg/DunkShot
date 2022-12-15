using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    [SerializeField] private Pool[] pools;

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

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        return go;
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
                    GameObject go = CreateGameObject(pool.prefab);
                    pool.listObject.Add(go);
                    go.SetActive(true);
                    return go;
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
        else if (instance is Obstacle)
        {
            (instance as Obstacle).Renew();
        }

        obj.transform.parent = transform;
        obj.SetActive(false);
    }

    public void RecallAll()
    {
        Debug.Log("ObjectPooler recall all!");

        foreach (var pool in pools)
        {
            foreach (var obj in pool.listObject)
            {
                Recall(obj);
            }
        }
    }
}