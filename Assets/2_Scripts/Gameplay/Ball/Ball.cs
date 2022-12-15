using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    private SpriteRenderer _renderer;
    private BallTail _tail;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _tail = GetComponentInChildren<BallTail>();

        Renew();
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        _rigidbody.simulated = true;
        _tail.Renew();
    }

    public void Appear()
    {
        _renderer.color = new Color(1f, 1f, 1f, 0f);
        _renderer.DOFade(1f, 0.3f);
    }

    public void Stop(Transform hoop)
    {
        transform.parent = hoop;
        transform.rotation = Quaternion.identity;

        _rigidbody.simulated = false;
        _rigidbody.angularVelocity = 0f;
        _rigidbody.velocity = Vector2.zero;
    }

    public void Push(Vector2 force)
    {
        transform.parent = null;
        _rigidbody.simulated = true;
        _rigidbody.angularVelocity = force.magnitude * 20f; // max = 750f
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
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
            _tail.Renew();
        }
    }
}