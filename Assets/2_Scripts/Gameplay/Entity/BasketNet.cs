using DG.Tweening;
using UnityEngine;

public class BasketNet : MonoBehaviour
{
    [SerializeField] private float maxScaleY; // độ giãn tối đa của lưới

    public void Renew()
    {
        transform.localScale = Vector3.one;
    }

    public void ScaleY(float distance)
    {
        float scaleY = Mathf.Min(maxScaleY, Mathf.Max(1f, 1f + distance / 6f));
        transform.localScale = new Vector3(1f, scaleY);
    }

    public void OnShootBall()
    {
        transform.DOScaleY(0.7f, 0.06f).SetEase(Ease.InSine).OnComplete(() =>
        {
            transform.DOScaleY(1f, 0.12f).SetEase(Ease.OutCirc);
        });
    }

    public void OnCancelShoot()
    {
        transform.DOScaleY(1f, 0.1f).SetEase(Ease.OutQuint);
    }

    public void OnReceiveBall()
    {
        transform.DOScaleY(1.2f, 0.05f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            transform.DOScaleY(1f, 0.05f).SetEase(Ease.InQuint);
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