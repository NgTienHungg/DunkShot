using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Basket : MonoBehaviour
{
    public BasketNet Net { get; private set; }
    public BasketHoop Hoop { get; private set; }
    public BasketEffect Effect { get; private set; }
    public BasketPoint Point { get; private set; }
    public BasketMovement Movement { get; private set; }
    public BasketObstacle Obstacle { get; private set; }

    [ShowInInspector] public bool IsGolden { get; private set; }

    private void Awake()
    {
        LoadComponent();
    }

    private void LoadComponent()
    {
        Net = GetComponentInChildren<BasketNet>();
        Hoop = GetComponentInChildren<BasketHoop>();
        Effect = GetComponentInChildren<BasketEffect>();
        Point = GetComponentInChildren<BasketPoint>();
        Movement = GetComponent<BasketMovement>();
        Obstacle = GetComponent<BasketObstacle>();
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        Net.Renew();
        Hoop.Renew();
        Effect.Renew();
        Point.Renew();
        Movement.Renew();
        Obstacle.Renew();
    }

    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
    }

    public void AppearInChallenge()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.6f).SetEase(Ease.OutQuart);
    }

    public void Disappear()
    {
        transform.DOScale(0f, 0.3f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ObjectPool.Instance.Recall(gameObject);
        });
    }

    public void GetScore()
    {
        Hoop.Inactive();
        Effect.Score();
        Movement.Stop();
        Obstacle.Free();
    }

    public void ReceiveBall(Ball ball)
    {
        transform.DOKill(); // kill anim xoay khi vừa bắn bóng (trong challenge)
        transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutBack);
        ball.Stop(transform);

        Net.OnReceiveBall(ball);
        Point.SetActiveCollider(false);
    }

    public void Aiming(float angle)
    {
        transform.DOKill();
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void ShootBall()
    {
        // wait 0.1f to enable collider (check event ball in basket)
        Invoke(nameof(ActivePoint), 0.1f);

        Net.OnShootBall();

        if (GameManager.Instance.Mode == GameMode.Challenge)
        {
            transform.DORotate(Vector3.zero, 2f).SetEase(Ease.OutExpo);
        }
    }

    private void ActivePoint()
    {
        Point.SetActiveCollider(true);
    }

    public void CancelShoot()
    {
        Net.OnCancelShoot();
    }

    public void SetGolden()
    {
        IsGolden = true;
        Hoop.SetGolden();
        Net.SetGolden();
    }
}