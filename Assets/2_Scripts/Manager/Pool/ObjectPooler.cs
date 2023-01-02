using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance { get => _instance; }

    [SerializeField] private Pool[] Pools;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (var pool in Pools)
        {
            pool.ListObject = new List<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                pool.ListObject.Add(CreateGameObject(pool.Prefab));
            }
        }
    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.parent = transform;
        obj.SetActive(false);
        return obj;
    }

    public GameObject Spawn(string tag)
    {
        foreach (var pool in Pools)
        {
            if (pool.Prefab.name == tag)
            {
                foreach (var obj in pool.ListObject)
                {
                    if (!obj.activeInHierarchy)
                    {
                        obj.SetActive(true);
                        return obj;
                    }
                }

                // expand pool
                if (pool.Expandable)
                {
                    GameObject obj = CreateGameObject(pool.Prefab);
                    pool.ListObject.Add(obj);
                    obj.SetActive(true);
                    return obj;
                }
                else
                {
                    Debug.LogWarning("The pool with tag " + tag + " is not expandable!");
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

        foreach (var pool in Pools)
        {
            foreach (var obj in pool.ListObject)
            {
                Recall(obj);
            }
        }
    }
}