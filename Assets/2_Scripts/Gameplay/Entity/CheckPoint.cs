using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Basket basket;
    [SerializeField] private EdgeCollider2D edgeCollider;

    private bool hasScore;

    public EdgeCollider2D Edge
    {
        get { return edgeCollider; }
    }

    private void Start()
    {
        Renew();
    }

    public void Renew()
    {
        this.hasScore = true;
        Edge.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            basket.ReceiveBall();
            collision.gameObject.GetComponent<Ball>().Stop(basket.transform);

            GameEvent.BallInHoop?.Invoke();

            if (this.hasScore)
            {
                GameEvent.GetScore?.Invoke();
                this.hasScore = false;
            }
        }
    }
}
