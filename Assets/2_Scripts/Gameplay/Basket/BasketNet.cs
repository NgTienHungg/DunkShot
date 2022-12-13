﻿using DG.Tweening;
using UnityEngine;

public class BasketNet : MonoBehaviour
{
    [Header("Drag basket")]
    [SerializeField] private float maxScaleY;

    [Header("Anchor Ball")]
    [SerializeField] private Transform anchor;
    [SerializeField] private Transform bottom;

    private Ball _ball;
    private bool _hasBall;
    private Vector3 _distance;


    public void Renew()
    {
        transform.localScale = Vector3.one;
        _ball = null;
        _hasBall = false;
    }

    private void Start()
    {
        _distance = anchor.localPosition - bottom.localPosition;
    }

    private void LateUpdate()
    {
        if (_hasBall)
        {
            anchor.localPosition = bottom.localPosition + _distance  / transform.localScale.y;
            _ball.transform.position = anchor.position;
        }
    }

    public void ScaleY(float distance)
    {
        float scaleY = Mathf.Min(maxScaleY, Mathf.Max(1f, 1f + distance / 6f));
        transform.localScale = new Vector3(1f, scaleY);
    }

    public void OnShootBall()
    {
        _ball = null;
        _hasBall = false;

        transform.DOScaleY(0.7f, 0.06f).SetEase(Ease.InSine).OnComplete(() =>
        {
            transform.DOScaleY(1f, 0.12f).SetEase(Ease.OutCirc);
        });
    }

    public void OnCancelShoot()
    {
        transform.DOScaleY(1f, 0.1f).SetEase(Ease.OutQuint);
    }

    public void OnReceiveBall(Ball ball)
    {
        _ball = ball;
        _hasBall = true;

        transform.DOScaleY(1.2f, 0.08f).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            transform.DOScaleY(1f, 0.06f).SetEase(Ease.InExpo);
        });
    }

    public void OnCollisionWithBall()
    {
        transform.DOScaleY(0.9f, 0.06f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            transform.DOScaleY(1f, 0.06f).SetEase(Ease.OutCirc);
        });
    }
}