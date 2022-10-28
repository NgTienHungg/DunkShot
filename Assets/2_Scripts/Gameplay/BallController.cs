using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private Vector3 offset;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NetHoop"))
        {
            Debug.Log("On net");
            rigidBody.bodyType = RigidbodyType2D.Static;
        }
    }
}