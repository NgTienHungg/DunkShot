using DG.Tweening;
using UnityEngine;

public class Net : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 bottomPoint;
    private Ball ball;

    //private float distanceWithBot = 

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bottomPoint = transform.GetChild(0).position;
    }

    private void Update()
    {
        //if (ball == null)
        //    return;

        //float netHeight = spriteRenderer.bounds.size.y * transform.localScale.y;
        //bottomPoint = transform.position - new Vector3(0f, netHeight);

        //ball.transform.position = bottomPoint + new Vector3(0f, distanceWithBottom);
        //ball.GetRigidbody().angularDrag = 0f;
        float angle = Mathf.Abs(360 - transform.eulerAngles.z) * Mathf.Deg2Rad;
        float huyen = spriteRenderer.bounds.size.y;
        float height = huyen * Mathf.Cos(angle);

        Debug.Log("Net height: " + huyen);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ball = collision.gameObject.GetComponent<Ball>();
        }
    }

    public void Elastic()
    {
        transform.DOScaleY(1f, 0.12f).SetEase(Ease.OutQuint);
    }
}