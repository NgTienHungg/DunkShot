using UnityEngine;

public class Mechanic : MonoBehaviour
{
    private Ball ball;
    private Basket basket;
    [SerializeField] private Trajectory trajectory;

    [SerializeField] private float pushForce, minForce;
    [SerializeField] private float maxDistance;

    private bool active; // receive Input event
    private bool isAiming; // to rotate hoop
    private bool canShoot; // show or hide trajectory and push ball

    private Vector3 startPoint, endPoint;
    private Vector3 direction, force;
    private float distance;

    private void OnEnable()
    {
        GameEvent.BasketReceiveBall += ActiveMechanic;
    }

    private void OnDisable()
    {
        GameEvent.BasketReceiveBall -= ActiveMechanic;
    }

    private void Start()
    {
        active = false;
        isAiming = false;
        canShoot = false;
    }

    private void Update()
    {
        if (!active)
            return;

        if (Input.GetMouseButtonDown(0) && !isAiming)
            StartAiming();

        if (Input.GetMouseButtonUp(0) && isAiming)
            Shoot();

        if (isAiming)
            Aiming();
    }

    private void ActiveMechanic()
    {
        active = true;
    }

    public void SetBall(Ball ball)
    {
        this.ball = ball;
    }

    public void SetBasket(Basket basket)
    {
        this.basket = basket;
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

        // trajectory
        canShoot = force.magnitude >= minForce;
        if (canShoot)
        {
            trajectory.Show();
            trajectory.Simulate(ball.transform.position, force);
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
            ball.Push(force);
            basket.ShootBall();
            active = false;
        }
        else
        {
            basket.CancelShoot();
        }
    }
}