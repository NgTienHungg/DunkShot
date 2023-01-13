﻿using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class BasketSpawner : MonoBehaviour
{
    [Header("Setup gameplay")]
    [SerializeField] private Vector2 _firstBasketPos = new Vector2(-2.7f, -2.8f);
    [SerializeField] private Vector2 _secondBasketPos = new Vector2(2f, 0.3f);

    [Header("Range Spawn")]
    [SerializeField] private Vector2 _rangePositionX = new Vector2(1.8f, 3f);
    [SerializeField] private Vector2 _rangeDistanceY = new Vector2(2.5f, 3.5f);
    [SerializeField] private Vector2 _rangeAngleZ = new Vector2(10f, 45f);

    [Header("Challenge")]
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private GameObject _level;
    private List<Basket> _listBasket;
    private List<GameObject> _listObject;

    private Basket _lastBasket;
    public Basket LastBasket { get => _lastBasket; }

    private Basket _currentBasket;
    public Basket CurrentBasket { get => _currentBasket; }

    private Basket _nextBasket;
    public Basket NextBasket { get => _nextBasket; }

    private bool _spawnInLeft;

    private void OnEnable()
    {
        Observer.OnSetupScene += SpawnBasket;
        Observer.OnStartGame += BasketAppear;
        Observer.OnStartChallenge += SetupLevel;
        Observer.BallInBasketHasPoint += ChangeTargetBasket;
        Observer.BallInBasketHasPointInChallenge += ChangeTargetBasketInChallenge;
    }

    private void SpawnBasket()
    {
        _currentBasket = ObjectPool.Instance.Spawn(PoolTag.BASKET).GetComponent<Basket>();
        _currentBasket.transform.position = _firstBasketPos;
        _currentBasket.Point.HasPoint = false; // the first basket has no point
        _nextBasket = ObjectPool.Instance.Spawn(PoolTag.BASKET).GetComponent<Basket>();
        _nextBasket.transform.position = _secondBasketPos;
        _lastBasket = _currentBasket;
        _spawnInLeft = true;
    }

    private void BasketAppear()
    {

    }

    private void SetupLevel()
    {
        DestroyImmediate(_level);
        _level = Instantiate(_levelPrefab);
        _listObject = new List<GameObject>();
        _listBasket = new List<Basket>();

        foreach (Transform child in _level.transform)
        {
            _listObject.Add(child.gameObject);

            if (child.GetComponent<MonoBehaviour>() is Basket)
            {
                _listBasket.Add(child.GetComponent<Basket>());
            }
        }

        _currentBasket = _listBasket[0];
        _currentBasket.Hoop.Inactive();
        _currentBasket.Point.HasPoint = false;
        _nextBasket = _listBasket[1];
        _lastBasket = _currentBasket;
        _listBasket[_listBasket.Count - 1].SetGolden();
    }

    private void ChangeTargetBasket()
    {
        _currentBasket.Disappear();
        _currentBasket = _nextBasket;
        _lastBasket = _currentBasket;

        SpawnNextBasket();
        _nextBasket.Appear();

        int rnd = Random.Range(0, 100);
        if (rnd <= 50)
            ObstacleSpawner.Instance.Spawn(_nextBasket);
        else if (rnd <= 70)
            _nextBasket.Movement.Move();
    }

    private void SpawnNextBasket()
    {
        _nextBasket = ObjectPool.Instance.Spawn(PoolTag.BASKET).GetComponent<Basket>();

        float positionX, distanceY, angleZ;

        if (!_spawnInLeft)
        {
            positionX = Random.Range(_rangePositionX.x, _rangePositionX.y);
            distanceY = Random.Range(_rangeDistanceY.x, _rangeDistanceY.y);
            angleZ = Random.Range(_rangeAngleZ.x, _rangeAngleZ.y);
        }
        else
        {
            positionX = Random.Range(-_rangePositionX.y, -_rangePositionX.x);
            distanceY = Random.Range(_rangeDistanceY.x, _rangeDistanceY.y);
            angleZ = Random.Range(-_rangeAngleZ.y, -_rangeAngleZ.x);
        }

        _nextBasket.transform.position = new Vector3(positionX, _currentBasket.transform.position.y + distanceY);
        _nextBasket.transform.eulerAngles = new Vector3(0f, 0f, angleZ);

        // swap side to spawn
        _spawnInLeft = !_spawnInLeft;
    }

    private void ChangeTargetBasketInChallenge()
    {
        _currentBasket.transform.DORotate(Vector3.zero, 0.4f).SetDelay(0.5f).SetEase(Ease.OutExpo);

        for (int i = 0; i < _listBasket.Count; i++)
        {
            Basket basket = _listBasket[i];

            if (!basket.gameObject.activeInHierarchy)
                continue;

            if (basket.transform.childCount == 6)
            {
                _currentBasket = basket;
                // chỉ cho phép rơi xuống tối thiểu 3 basket
                if (i >= 2)
                {
                    _lastBasket = _listBasket[i - 2];
                    foreach (var obj in _listObject)
                    {
                        if (!obj.activeInHierarchy)
                            continue;
                        if (obj.transform.position.y < _lastBasket.transform.position.y)
                            obj.SetActive(false);
                    }
                }
            }
        }
    }
}