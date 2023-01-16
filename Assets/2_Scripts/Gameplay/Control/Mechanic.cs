﻿using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private float pushForce, minForce;
    [SerializeField] private float maxDistance;

    private Ball _ball;
    public Ball Ball { get => _ball; }

    private Basket _basket;

    [SerializeField]
    private Trajectory trajectory;

    private bool _isAiming;
    private bool _canAim, _canShoot;

    private Vector3 _startPoint, _endPoint;
    private Vector3 _direction, _force;
    private float _distance;

    private void Awake()
    {
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.BallInBasket += SetCanAim;
        Observer.BallInBasketInChallenge += SetCanAim;
    }

    public void SetupBall()
    {
        _ball = ObjectPool.Instance.Spawn(PoolTag.BALL).GetComponent<Ball>();
        _ball.transform.position = GameController.Instance.BasketControl.CurrentBasket.transform.position + new Vector3(0f, 2.5f);
        _ball.Appear();

        _canAim = false;
        _isAiming = false;
        _canShoot = false;
    }

    private void Update()
    {
        if (!GameController.Instance.IsPlaying)
            return;

        if (_ball.transform.position.y < GameController.Instance.BasketControl.LastBasket.transform.position.y - 4f)
        {
            if (GameManager.Instance.Mode == GameMode.Endless)
                Observer.BallDead?.Invoke();
            else if (GameManager.Instance.Mode == GameMode.Challenge)
                Observer.BallDeadInChallenge?.Invoke();
        }

        if (_canAim)
        {
            if (Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject() && !_isAiming)
                StartAiming();

            if (Input.GetMouseButtonUp(0) && _isAiming)
                Shoot();

            if (_isAiming)
                Aiming();
        }
    }

    private void SetCanAim()
    {
        _basket = GameController.Instance.BasketControl.CurrentBasket;
        _canAim = true;
    }

    private void StartAiming()
    {
        _isAiming = true;
        _startPoint = Util.GetMouseWorldPosition();
    }

    private void Aiming()
    {
        _endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _distance = Mathf.Min(maxDistance, Vector3.Distance(_startPoint, _endPoint));
        _direction = (_startPoint - _endPoint).normalized;
        _force = _direction * _distance * pushForce;

        if (_force.magnitude < 4f) return;

        // calculate angle of hoop
        float aimingAngle = Vector3.Angle(_force, Vector3.up);
        float sign = _endPoint.x > _startPoint.x ? 1 : -1;
        _basket.transform.eulerAngles = new Vector3(0f, 0f, sign * aimingAngle);

        // calculate net scale
        _basket.Net.ScaleY(_distance);

        _canShoot = _force.magnitude >= minForce;

        // trajectory
        if (_canShoot)
        {
            trajectory.Show();
            trajectory.Simulate(_ball.transform.position, _force);
        }
        else
        {
            trajectory.Hide();
        }
        Debug.DrawLine(_startPoint, _endPoint, Color.red);
    }

    private void Shoot()
    {
        _isAiming = false;
        trajectory.Hide();

        if (_canShoot)
        {
            _ball.Push(_force);
            _basket.ShootBall();
            _canAim = false;
        }
        else
        {
            _basket.CancelShoot();
        }
    }
}