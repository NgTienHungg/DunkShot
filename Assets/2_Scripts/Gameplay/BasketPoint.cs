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
            basket.ReceiveBall();
            collision.gameObject.GetComponent<Ball>().Stop(basket.transform);

            Observer.BasketReceiveBall?.Invoke();

            if (this.hasPoint)
            {
                Observer.GetScore?.Invoke();
                basket.Hoop.OnGetScore();
                basket.Point.SetHasPoint(false);
            }
        }
    }
}