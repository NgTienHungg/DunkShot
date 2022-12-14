using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Pool
{
    public GameObject prefab;
    public ObjectTag objectTag;
    public int poolSize;
    public bool expandable = true;

    [HideInInspector]
    public List<GameObject> listObject;
}