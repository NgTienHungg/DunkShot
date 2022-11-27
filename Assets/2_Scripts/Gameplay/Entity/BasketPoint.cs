using UnityEngine;

public class BasketPoint : MonoBehaviour
{
    [SerializeField] private Basket basket;
    [SerializeField] private EdgeCollider2D edgeCollider;

    public bool hasPoint;

    private void Start()
    {
        Renew();
    }

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

            GameEvent.BasketReceiveBall?.Invoke();

            if (this.hasPoint)
            {
                GameEvent.GetScore?.Invoke();
                this.hasPoint = false;
            }
        }
    }
}