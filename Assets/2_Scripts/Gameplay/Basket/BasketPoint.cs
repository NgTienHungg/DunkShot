using UnityEngine;

public class BasketPoint : MonoBehaviour
{
    private Basket _basket;

    private EdgeCollider2D _collider;

    private bool _hasPoint = true;

    private void Awake()
    {
        _basket = GetComponentInParent<Basket>();
        _collider = GetComponent<EdgeCollider2D>();
    }

    public void Renew()
    {
        SetHasPoint(true);
        SetActiveCollider(true);
    }

    public void SetHasPoint(bool hasPoint)
    {
        _hasPoint = hasPoint;
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

            if (_hasPoint)
            {
                _basket.ReceivePoint();
            }

            _basket.ReceiveBall(collision.gameObject.GetComponent<Ball>());

            Observer.BallInBasket?.Invoke();
        }
    }
}