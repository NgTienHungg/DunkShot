using DG.Tweening;
using UnityEngine;

public class BasketNet : MonoBehaviour
{
    [Header("Drag basket")]
    [SerializeField] private float maxScaleY = 1.85f;

    [Header("Anchor Ball")]
    [SerializeField] private Transform anchor;
    [SerializeField] private Transform bottom;
    private Vector3 distance;

    private Ball ball;
    private bool hasBall;

    private void Start()
    {
        distance = anchor.localPosition - bottom.localPosition;
    }

    private void Update()
    {
        if (hasBall)
        {
            anchor.localPosition = bottom.localPosition + distance  / transform.localScale.y;
            ball.transform.position = anchor.position;
        }
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        ball = null;
        hasBall = false;
    }

    public void ScaleY(float distance)
    {
        float scaleY = Mathf.Min(maxScaleY, Mathf.Max(1f, 1f + distance / 6f));
        transform.localScale = new Vector3(1f, scaleY);
    }

    public void OnShootBall()
    {
        ball = null;
        hasBall = false;

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
        this.ball = ball;
        hasBall = true;

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