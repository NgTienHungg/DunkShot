using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private Vector3 posInBasket = new Vector3(0f, -0.45f);

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        rigidBody.simulated = true;
    }

    public void Stop(Transform hoop)
    {
        transform.parent = hoop;
        transform.DOLocalMove(posInBasket, 0.15f);

        rigidBody.simulated = false;
        rigidBody.angularVelocity = 0f;
        rigidBody.velocity = Vector2.zero;
    }

    public void Push(Vector2 force)
    {
        rigidBody.simulated = true;
        rigidBody.angularVelocity = 500f;
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
        }
    }
}