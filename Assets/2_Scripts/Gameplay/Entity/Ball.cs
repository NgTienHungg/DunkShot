using UnityEngine;

public class Ball : MonoBehaviour
{
    public static float GravityScale;

    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        Ball.GravityScale = rigidBody.gravityScale;
    }

    public void Stop()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
    }

    public void Push(Vector2 force)
    {
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Net"))
        {
            Debug.Log("On net");
        }
    }
}