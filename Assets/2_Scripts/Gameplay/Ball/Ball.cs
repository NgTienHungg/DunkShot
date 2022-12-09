using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private BallTail tail;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Renew();
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        rigidBody.simulated = true;
        tail.Renew();
    }

    public void Appear()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        spriteRenderer.DOFade(1f, 0.3f);
    }

    public void Stop(Transform hoop)
    {
        transform.parent = hoop;
        transform.rotation = Quaternion.identity;

        rigidBody.simulated = false;
        rigidBody.angularVelocity = 0f;
        rigidBody.velocity = Vector2.zero;
    }

    public void Push(Vector2 force)
    {
        transform.parent = null;
        rigidBody.simulated = true;
        rigidBody.angularVelocity = force.magnitude * 30f; // max = 750f
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("collide obstacle");
            Observer.BallCollideObstacle?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Hoop"))
        {
            Debug.Log("collide hoop");
            Observer.BallCollideHoop?.Invoke();
            tail.Renew();
        }
    }
}