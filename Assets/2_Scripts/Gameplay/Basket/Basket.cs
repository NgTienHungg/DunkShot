using UnityEngine;
using DG.Tweening;

public class Basket : MonoBehaviour
{
    private BasketNet _net;
    public BasketNet Net { get => _net; }

    private BasketHoop _hoop;
    public BasketHoop Hoop { get => _hoop; }

    private BasketEffect _effect;
    public BasketEffect Effect { get => _effect; }

    private BasketPoint _point;
    public BasketPoint Point { get => _point; }

    private BasketMovement _movement;
    public BasketMovement Movement { get => _movement; }

    private BasketObstacle _obstacle;
    public BasketObstacle Obstacle { get => _obstacle; }

    private void Awake()
    {
        _net = GetComponentInChildren<BasketNet>();
        _hoop = GetComponentInChildren<BasketHoop>();
        _effect = GetComponentInChildren<BasketEffect>();
        _point = GetComponentInChildren<BasketPoint>();

        _movement = GetComponent<BasketMovement>();
        _obstacle = GetComponent<BasketObstacle>();
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        _net.Renew();
        _hoop.Renew();
        _effect.Renew();
        _point.Renew();

        _movement.Renew();
        _obstacle.Renew();
    }


    #region Animation
    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
    }

    public void Disappear()
    {
        transform.DOScale(0f, 0.3f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ObjectPool.Instance.Recall(gameObject);
        });
    }
    #endregion


    #region Shoot Ball
    public void GetScore()
    {
        _hoop.Inactive();
        _effect.Score();
        _movement.Stop();
        _obstacle.Free();
    }

    public void ReceiveBall(Ball ball)
    {
        Controller.Instance.Mechanic.SetBasket(this);
        transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
        ball.Stop(transform);

        _net.OnReceiveBall(ball);
        _point.SetActiveCollider(false);
    }

    public void ShootBall()
    {
        // wait 0.1f to enable collider (check event ball in basket)
        transform.DOScale(1f, 0.1f).SetUpdate(true).OnComplete(() =>
        {
            _point.SetActiveCollider(true);
        });

        _net.OnShootBall();
    }

    public void CancelShoot()
    {
        _net.OnCancelShoot();
    }
    #endregion
}