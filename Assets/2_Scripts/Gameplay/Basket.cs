using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Basket : MonoBehaviour
{
    [SerializeField] private BasketNet net;
    [SerializeField] private BasketHoop hoop;
    [SerializeField] private BasketPoint point;

    public BasketNet Net { get { return net; } }
    public BasketHoop Hoop { get { return hoop; } }
    public BasketPoint Point { get { return point; } }

    public static void Recall(Basket basket)
    {
        basket.Renew();
        ObjectPooler.Instance.Recall(basket.gameObject);
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;

        net.Renew();
        hoop.Renew();
        point.Renew();
    }

    public void Rotate(float angle)
    {
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void Appear()
    {
        transform.DOScale(Vector3.zero, 0f);
        transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack);
    }

    public void Disappear()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            Renew();
            ObjectPooler.Instance.Recall(gameObject);
        });
    }

    public void ReceiveBall()
    {
        Controller.Instance.mechanic.SetBasket(this);
        transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
        net.OnReceiveBall();
        point.SetActiveCollider(false);
    }

    public void ShootBall()
    {
        StartCoroutine(WaitToEnableCheckPoint());
        net.OnShootBall();
    }

    private IEnumerator WaitToEnableCheckPoint()
    {
        float waitTime = 0.1f;
        yield return new WaitForSecondsRealtime(waitTime);
        point.SetActiveCollider(true);
    }

    public void CancelShoot()
    {
        net.OnCancelShoot();
    }
}