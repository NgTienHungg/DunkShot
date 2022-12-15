using UnityEngine;
using DG.Tweening;

public class Basket : MonoBehaviour
{
    public BasketNet Net { get { return _net; } }
    public BasketHoop Hoop { get { return _hoop; } }
    public BasketPoint Point { get { return _point; } }
    public BasketMovement Movement { get { return _movement; } }
    public BasketObstacle Obstacle { get { return _obstacle; } }

    private BasketNet _net;
    private BasketHoop _hoop;
    private BasketPoint _point;

    private BasketMovement _movement;
    private BasketObstacle _obstacle;


    private void Awake()
    {
        _net = GetComponentInChildren<BasketNet>();
        _hoop = GetComponentInChildren<BasketHoop>();
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
            ObjectPooler.Instance.Recall(gameObject);
        });
    }
    #endregion


    #region Shoot Ball
    public void GetScore()
    {
        _hoop.Scale();
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