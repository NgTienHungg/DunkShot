using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class BasketSpawner : MonoBehaviour
{
    [Header("Prepare")]
    [SerializeField] private Vector2 _firstBasketPos = new Vector2(-2.7f, -2.8f);
    [SerializeField] private Vector2 _secondBasketPos = new Vector2(2f, 0.3f);

    [Header("Range spawn")]
    [SerializeField] private Vector2 _rangePositionX = new Vector2(1.8f, 3f);
    [SerializeField] private Vector2 _rangeDistanceY = new Vector2(2.5f, 3.5f);
    [SerializeField] private Vector2 _rangeAngleZ = new Vector2(10f, 45f);

    public Basket CurrentBasket { get => _currentBasket; }
    private Basket _currentBasket, _nextBasket;
    private bool _spawnInLeft;

    [Header("Challenge")]
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private GameObject _level;
    private List<Basket> _listBasket;

    private void OnEnable()
    {
        Observer.BallInBasketHasPoint += ChangeTargetBasket;
        Observer.BallInBasketHasPointInChallenge += ChangeTargetBasketInChallenge;
    }

    private void OnDisable()
    {
        Observer.BallInBasketHasPoint -= ChangeTargetBasket;
        Observer.BallInBasketHasPointInChallenge -= ChangeTargetBasketInChallenge;
    }

    public void Renew()
    {
        if (CanvasController.Instance.Mode == GameMode.Endless)
            PrepareBasket();
        else if (CanvasController.Instance.Mode == GameMode.Challenge)
            LoadLevel();
    }

    private void PrepareBasket()
    {
        _currentBasket = ObjectPool.Instance.Spawn(PoolTag.BASKET).GetComponent<Basket>();
        _currentBasket.transform.position = _firstBasketPos;
        _currentBasket.Point.HasPoint = false; // the first basket has no point

        _nextBasket = ObjectPool.Instance.Spawn(PoolTag.BASKET).GetComponent<Basket>();
        _nextBasket.transform.position = _secondBasketPos;

        _spawnInLeft = true;
    }

    private void LoadLevel()
    {
        DestroyImmediate(_level);

        _level = Instantiate(_levelPrefab);

        _listBasket = new List<Basket>();

        foreach (Transform child in _level.transform)
        {
            if (child.GetComponent<MonoBehaviour>() is Basket)
            {
                _listBasket.Add(child.GetComponent<Basket>());
            }
        }

        _currentBasket = _listBasket[0];
        _currentBasket.Point.HasPoint = false;
        _nextBasket = _listBasket[1];
        _listBasket[_listBasket.Count - 1].SetGolden();
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

    private void ChangeTargetBasket()
    {
        _currentBasket.Disappear();
        _currentBasket = _nextBasket;

        SpawnNextBasket();
        _nextBasket.Appear();

        int rnd = Random.Range(0, 100);
        if (rnd <= 50)
        {
            ObstacleSpawner.Instance.Spawn(_nextBasket);
        }
        else if (rnd <= 70)
        {
            _nextBasket.Movement.Move();
        }
    }

    private void ChangeTargetBasketInChallenge()
    {
        _currentBasket.transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);

        for (int i = 0; i < _listBasket.Count; i++)
        {
            if (_listBasket[i].transform.childCount == 6)
            {
                _currentBasket = _listBasket[i];

                if (i == _listBasket.Count - 1)
                {
                    Debug.LogWarning("pass challenge");
                    Observer.OnPassChallenge?.Invoke();
                }
                break;
            }
        }
    }

    public void PreparePlay()
    {
        _currentBasket.transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
    }
}