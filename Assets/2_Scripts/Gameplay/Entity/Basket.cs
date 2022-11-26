using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Basket : MonoBehaviour
{
    [SerializeField] private Net net;
    [SerializeField] private CheckPoint checkPoint;

    public void Renew()
    {
        transform.rotation = Quaternion.identity;
        net.Renew();
        checkPoint.Renew();
    }

    public void Rotate(float angle)
    {
        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    public void ScaleNet(float scaleY)
    {
        net.transform.localScale = new Vector3(1f, scaleY, 1f);
    }

    public void ReceiveBall()
    {
        transform.DORotate(Vector3.zero, 0.2f);
        checkPoint.Edge.enabled = false;
        Mechanic.Instance.hoop = this;
        net.OnReceiveBall();
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
        checkPoint.Edge.enabled = true;
    }

    public void CancelShoot()
    {
        net.OnCancelShoot();
    }
}