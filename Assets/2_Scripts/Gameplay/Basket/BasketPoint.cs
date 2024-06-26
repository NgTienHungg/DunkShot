﻿using UnityEngine;

public class BasketPoint : MonoBehaviour
{
    private Basket _basket;

    private EdgeCollider2D _collider;

    private bool _hasPoint = true;

    public bool HasPoint
    {
        get { return _hasPoint; }
        set { _hasPoint = value; }
    }

    private void Awake()
    {
        _basket = GetComponentInParent<Basket>();
        _collider = GetComponent<EdgeCollider2D>();
        _collider.isTrigger = true;
    }

    public void Renew()
    {
        _hasPoint = true;
        SetActiveCollider(true);
    }

    public void SetActiveCollider(bool status)
    {
        _collider.enabled = status;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // phát ra sự kiện để xử lý điểm trước
            // sau đó mới phát sự kiện BallInBasket để clear các điểm nhận được trong lượt này tại ScoreManager

            AudioManager.Instance.PlaySound(AudioKey.COLLISION_NET);
            _basket.ReceiveBall(collision.gameObject.GetComponent<Ball>());

            if (_hasPoint)
            {
                _hasPoint = false;
                _basket.GetScore();

                if (GameManager.Instance.Mode == GameMode.Endless)
                {
                    Observer.BallInBasketHasPoint?.Invoke();
                }
                else if (GameManager.Instance.Mode == GameMode.Challenge)
                {
                    Observer.BallInBasketHasPointInChallenge?.Invoke();

                    if (GetComponentInParent<Basket>().IsGolden)
                    {
                        Observer.BallInGoldenBasket?.Invoke();
                    }
                }
            }

            if (GameManager.Instance.Mode == GameMode.Endless)
            {
                Observer.BallInBasket?.Invoke();
            }
            else if (GameManager.Instance.Mode == GameMode.Challenge)
            {
                Observer.BallInBasketInChallenge?.Invoke();
            }
        }
    }
}