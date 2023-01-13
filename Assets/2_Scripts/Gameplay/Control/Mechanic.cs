using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private float pushForce, minForce;
    [SerializeField] private float maxDistance;

    private Ball _ball;
    private Basket _basket;

    [SerializeField]
    private Trajectory trajectory;

    private bool isAiming;
    private bool canAim, canShoot;

    private Vector3 startPoint, endPoint;
    private Vector3 direction, force;
    private float distance;

    private void Awake()
    {
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnStartGame += SetupGame;
        Observer.BallInBasketHasNoPoint += SetCanAim;
    }

    //private void OnDisable()
    //{
    //    Observer.BallInBasketHasNoPoint -= SetCanAim;
    //}

    private void SetupGame()
    {
        _basket = Controller.Instance.BasketSpawner.CurrentBasket;
        _ball = ObjectPool.Instance.Spawn(PoolTag.BALL).GetComponent<Ball>();
    }

    public void Renew()
    {
        _basket = Controller.Instance.BasketSpawner.CurrentBasket;

        if (_ball != null)
            ObjectPool.Instance.Recall(_ball.gameObject);

        _ball = ObjectPool.Instance.Spawn(PoolTag.BALL).GetComponent<Ball>();
        _ball.transform.position = new Vector3(_basket.transform.position.x, _basket.transform.position.y + 2.5f);
        _ball.Appear();

        CameraController.Instance.FollowBall();

        canAim = false;
        isAiming = false;
        canShoot = false;
    }

    private void Update()
    {
        if (!Controller.Instance.IsPlaying)
            return;

        if (Controller.Instance.IsPlaying && _ball.transform.position.y < Controller.Instance.BasketSpawner.LastBasket.transform.position.y - 4f)
        {
            CameraController.Instance.UnfollowBall();
            Observer.BallDead?.Invoke();
        }

        if (canAim)
        {
            if (Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject() && !isAiming)
                StartAiming();

            if (Input.GetMouseButtonUp(0) && isAiming)
                Shoot();

            if (isAiming)
                Aiming();
        }
    }

    private void SetCanAim()
    {
        canAim = true;
    }

    public void SetBasket(Basket basket)
    {
        this._basket = basket;
    }

    public Ball GetBall()
    {
        return _ball;
    }

    private void StartAiming()
    {
        isAiming = true;
        startPoint = Util.GetMouseWorldPosition();
    }

    private void Aiming()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Mathf.Min(maxDistance, Vector3.Distance(startPoint, endPoint));
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        if (force.magnitude < 4f) return;

        // calculate angle of hoop
        float aimingAngle = Vector3.Angle(force, Vector3.up);
        float sign = endPoint.x > startPoint.x ? 1 : -1;
        _basket.transform.eulerAngles = new Vector3(0f, 0f, sign * aimingAngle);

        // calculate net scale
        _basket.Net.ScaleY(distance);

        canShoot = force.magnitude >= minForce;

        // trajectory
        if (canShoot)
        {
            trajectory.Show();
            trajectory.Simulate(_ball.transform.position, force);
        }
        else
        {
            trajectory.Hide();
        }
        Debug.DrawLine(startPoint, endPoint, Color.red);
    }

    private void Shoot()
    {
        isAiming = false;
        trajectory.Hide();

        if (canShoot)
        {
            _ball.Push(force);
            _basket.ShootBall();
            canAim = false;
        }
        else
        {
            _basket.CancelShoot();
        }
    }
}