using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Quaternion _startRotation;

    private void Awake()
    {
        _startRotation = transform.rotation;
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = _startRotation;
        transform.DOKill(); // kill anim rotate
    }

    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutExpo).SetDelay(0.1f);
    }

    public void Disappear()
    {
        transform.DOKill(); // kill anim rotate
        transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ObjectPool.Instance.Recall(gameObject);
        });
    }
}