using UnityEngine;
using System.Collections.Generic;

public class BasketObstacle : MonoBehaviour
{
    private List<Obstacle> _listObstacle;

    private void Awake()
    {
        _listObstacle = new List<Obstacle>();
    }

    public void Renew()
    {
        foreach (var obstacle in _listObstacle)
        {
            ObjectPool.Instance.Recall(obstacle.gameObject);
        }

        _listObstacle.Clear();
    }

    public void Add(Obstacle obstacle)
    {
        _listObstacle.Add(obstacle);
    }

    public void Free()
    {
        foreach (var obstacle in _listObstacle)
        {
            obstacle.Disappear();
        }

        _listObstacle.Clear();
    }
}