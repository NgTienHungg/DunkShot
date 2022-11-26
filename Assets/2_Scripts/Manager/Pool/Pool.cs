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