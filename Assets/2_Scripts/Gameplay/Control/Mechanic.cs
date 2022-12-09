using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private float pushForce, minForce;
    [SerializeField] private float maxDistance;

    private Ball ball;
    private Basket basket;

    [SerializeField]
    private Trajectory trajectory;

    private bool isAiming;
    private bool canAim, canShoot;

    private Vector3 startPoint, endPoint;
    private Vector3 direction, force;
    private float distance;

    private void OnEnable()
    {
        Observer.BallInBasket += SetCanAim;
    }

    private void OnDisable()
    {
        Observer.BallInBasket -= SetCanAim;
    }

    public void Renew()
    {
        basket = Controller.Instance.BasketSpawner.GetCurrentBasket();

        if (ball != null)
            ObjectPooler.Instance.Recall(ball.gameObject);

        ball = ObjectPooler.Instance.Spawn(ObjectTag.Ball).GetComponent<Ball>();
        ball.transform.position = new Vector3(basket.transform.position.x, basket.transform.position.y + 2.5f);
        ball.Appear();

        CameraController.Instance.FollowBall();

        canAim = false;
        isAiming = false;
        canShoot = false;
    }

    private void Update()
    {
        if (!Controller.Instance.IsPlaying)
            return;

        if (Controller.Instance.IsPlaying && ball.transform.position.y < basket.transform.position.y - 4f)
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
        this.basket = basket;
    }

    public Ball GetBall()
    {
        return ball;
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

        // calculate angle of hoop
        float aimingAngle = Vector3.Angle(force, Vector3.up);
        float sign = endPoint.x > startPoint.x ? 1 : -1;
        basket.Rotate(sign * aimingAngle);

        // calculate net scale
        basket.Net.ScaleY(distance);

        canShoot = force.magnitude >= minForce;

        // trajectory
        if (canShoot)
        {
            trajectory.ShowTrajectory();
            trajectory.Simulate(ball.transform.position, force);
        }
        else
        {
            trajectory.HideTrajectory();
        }
        Debug.DrawLine(startPoint, endPoint, Color.red);
    }

    private void Shoot()
    {
        isAiming = false;
        trajectory.HideTrajectory();

        if (canShoot)
        {
            ball.Push(force);
            basket.ShootBall();
            canAim = false;
        }
        else
        {
            basket.CancelShoot();
        }
    }
}