using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;

    private Vector3 posInBasket = new Vector3(0f, -0.5f);

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
        rigidBody.angularVelocity = 1000f;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
        GameEvent.ShootBall?.Invoke();
    }
}