using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

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

        LoadSkin();
        Renew();
    }

    private void OnEnable()
    {
        Observer.ChangeBallSkin += LoadSkin;
    }

    public void LoadSkin()
    {
        _renderer.sprite = DataManager.Instance.BallSkinInUse.Data.Sprite;
        _tail.LoadSkin();
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        _rigidbody.simulated = true;
        _tail.Renew();
    }

    [Button("FLame")]
    public void Flame()
    {
        _tail.Flaming();
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
        _rigidbody.angularVelocity = force.magnitude * 35f; // max = 750f
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Observer.BallCollideObstacle?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Hoop"))
        {
            Observer.BallCollideHoop?.Invoke();
            _tail.Renew();
        }
    }
}