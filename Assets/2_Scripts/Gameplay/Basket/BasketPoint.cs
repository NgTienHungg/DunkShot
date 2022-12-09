using UnityEngine;

public class BasketPoint : MonoBehaviour
{
    [SerializeField] private Basket basket;
    [SerializeField] private EdgeCollider2D edgeCollider;

    private bool hasPoint = true;

    public void Renew()
    {
        SetHasPoint(true);
        SetActiveCollider(true);
    }

    public void SetHasPoint(bool hasPoint)
    {
        this.hasPoint = hasPoint;
    }

    public void SetActiveCollider(bool status)
    {
        edgeCollider.enabled = status;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // phát ra sự kiện để xử lý điểm trước
            // sau đó mới phát sự kiện BallInBasket để clear các điểm nhận được trong lượt này tại ScoreManager

            if (hasPoint)
            {
                Observer.GetScore?.Invoke();
                basket.Hoop.OnGetScore();
                basket.Point.SetHasPoint(false);
            }

            basket.ReceiveBall(collision.gameObject.GetComponent<Ball>());
            collision.gameObject.GetComponent<Ball>().Stop(basket.transform);
            Observer.BallInBasket?.Invoke();
        }
    }
}