﻿using DG.Tweening;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    [Header("Prepare")]
    [SerializeField] private Vector2 firstBasketPos;
    [SerializeField] private Vector2 secondBasketPos;

    [Header("Range spawn")]
    [SerializeField] private Vector2 rangePositionX;
    [SerializeField] private Vector2 rangeDistanceY;
    [SerializeField] private Vector2 rangeAngleZ;

    public Basket CurrentBasket { get { return _currentBasket; } }

    private Basket _currentBasket, _nextBasket;

    private bool _spawnInLeft;


    private void OnEnable()
    {
        Observer.GetScore += ChangeTargetBasket;
    }

    private void OnDisable()
    {
        Observer.GetScore -= ChangeTargetBasket;
    }


    public void Renew()
    {
        _currentBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();
        _currentBasket.transform.position = firstBasketPos;
        _currentBasket.Point.HasPoint = false; // the first basket has no point

        _nextBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();
        _nextBasket.transform.position = secondBasketPos;

        _spawnInLeft = true;
    }


    private void SpawnNextBasket()
    {
        _nextBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();

        float positionX, distanceY, angleZ;

        if (!_spawnInLeft)
        {
            positionX = Random.Range(rangePositionX.x, rangePositionX.y);
            distanceY = Random.Range(rangeDistanceY.x, rangeDistanceY.y);
            angleZ = Random.Range(rangeAngleZ.x, rangeAngleZ.y);
        }
        else
        {
            positionX = Random.Range(-rangePositionX.y, -rangePositionX.x);
            distanceY = Random.Range(rangeDistanceY.x, rangeDistanceY.y);
            angleZ = Random.Range(-rangeAngleZ.y, -rangeAngleZ.x);
        }

        _nextBasket.transform.position = new Vector3(positionX, _currentBasket.transform.position.y + distanceY);
        _nextBasket.transform.eulerAngles = new Vector3(0f, 0f, angleZ);

        // swap side to spawn
        _spawnInLeft = !_spawnInLeft;
    }

    private void ChangeTargetBasket()
    {
        _currentBasket.Disappear();
        _currentBasket = _nextBasket;

        SpawnNextBasket();
        _nextBasket.Appear();

        ObstacleSpawner.Instance.Spawn(_nextBasket);
    }

    public void PreparePlay()
    {
        _currentBasket.transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
    }
}