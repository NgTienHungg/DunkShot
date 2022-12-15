using DG.Tweening;
using UnityEngine;

public class BasketMovement : MonoBehaviour
{
    [SerializeField] private float _timeMoving;

    private BasketTrajectory _trajectory;

    private bool _isMoving;

    public void Renew()
    {
        Stop();
    }

    public void Move()
    {
        _trajectory = ObjectPooler.Instance.Spawn(ObjectTag.BasketTrajectory).GetComponent<BasketTrajectory>();
        _trajectory.transform.position = new Vector3(Mathf.Sign(transform.position.x) * 2.15f, transform.position.y);
        _trajectory.Appear();

        float start, end;
        start = Mathf.Sign(transform.position.x) * (_trajectory.Length / 2 - 0.5f); // basket được spawn bên nào thì sẽ bắt đầu di chuyển từ bên đó
        end = -start;

        transform.parent = _trajectory.transform;
        transform.localPosition = new Vector3(start, 0f);

        transform.DOLocalMoveX(end, _timeMoving).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        _isMoving = true;
    }

    public void Stop()
    {
        if (_isMoving)
        {
            transform.DOKill();
            transform.parent = ObjectPooler.Instance.transform;

            _trajectory.Disappear();
            _isMoving = false;
        }
    }
}