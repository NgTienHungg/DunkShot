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

        LoadSkin();
        Renew();

        Observer.OnChangeSkin += LoadSkin;
    }

    public void LoadSkin()
    {
        _renderer.sprite = DataManager.Instance.SkinInUse.Data.Sprite;
        _tail.LoadSkin();
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

    public void Stop(Transform basket)
    {
        transform.parent = basket;
        transform.rotation = Quaternion.identity;

        _rigidbody.simulated = false;
        _rigidbody.angularVelocity = 0f;
        _rigidbody.velocity = Vector2.zero;
    }

    public void Push(Vector2 force)
    {
        if (force.magnitude < 20)
        {
            if (_tail.IsFlaming)
                AudioManager.Instance.PlaySound(AudioKey.RELEASE_FIRE_BALL_1);
            else
                AudioManager.Instance.PlaySound(AudioKey.RELEASE_BALL_1);
        }
        else if (force.magnitude < 25)
        {
            if (_tail.IsFlaming)
                AudioManager.Instance.PlaySound(AudioKey.RELEASE_FIRE_BALL_2);
            else
                AudioManager.Instance.PlaySound(AudioKey.RELEASE_BALL_2);
        }
        else
        {
            if (_tail.IsFlaming)
                AudioManager.Instance.PlaySound(AudioKey.RELEASE_FIRE_BALL_3);
            else
                AudioManager.Instance.PlaySound(AudioKey.RELEASE_BALL_3);
        }

        transform.parent = null;
        _rigidbody.simulated = true;
        _rigidbody.angularVelocity = force.magnitude * Random.Range(25f, 35f);
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.Instance.PlaySound(AudioKey.COLLISION_WALL);
            Observer.BallCollideObstacle?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Hoop"))
        {
            AudioManager.Instance.PlaySound(AudioKey.COLLISION_HOOP);
            Observer.BallCollideHoop?.Invoke();
            _tail.Renew();
        }
    }
}