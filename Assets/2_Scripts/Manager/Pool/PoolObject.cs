using UnityEngine;
using System.Collections.Generic;

public enum ObjectTag
{
    Ball = 0,
    Hoop,
    Star
}

public class PoolObject : MonoBehaviour
{
    public GameObject prefab;
    public ObjectTag objectTag;
    public int poolSize;
    public bool expandable;

    [HideInInspector]
    public List<GameObject> objects;
}