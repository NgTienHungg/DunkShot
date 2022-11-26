using UnityEngine;
using Cinemachine;

public class Mechanic : MonoBehaviour
{
    #region Singleton
    public static Mechanic Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    #endregion

    [HideInInspector] public Ball ball;
    [HideInInspector] public Basket hoop;

    [SerializeField] private Trajectory trajectory;
    [SerializeField] private BasketSpawner basketSpawner;
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    [SerializeField] private float netMaxElongation = 1.85f; // độ giãn tối đa của lưới
    [SerializeField] private float pushForce, minForce;
    [SerializeField] private float maxDistance;

    private bool active;
    private bool isAiming; // to rotate hoop
    private bool canShoot; // show or hide trajectory and push ball

    private Vector3 startPoint, endPoint;
    private Vector3 direction, force;
    private float distance;

    private void OnEnable()
    {
        GameEvent.BallInHoop += ActiveMechanic;
    }

    private void OnDisable()
    {
        GameEvent.BallInHoop -= ActiveMechanic;
    }

    private void Start()
    {
        SpawnBall();
        active = false;
        isAiming = false;
        canShoot = false;
    }

    private void SpawnBall()
    {
        Debug.Log("Spawn ball");
        Vector3 pos = basketSpawner.GetCurrentBasketPos();
        ball = ObjectPooler.Instance.Spawn(ObjectTag.Ball).GetComponent<Ball>();
        ball.transform.position = new Vector3(pos.x, pos.y + 1f);
        virtualCam.Follow = ball.transform;
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
        canShoot = force.magnitude >= minForce;

        // calculate angle of hoop
        float aimingAngle = Vector3.Angle(force, Vector3.up);
        float sign = endPoint.x > startPoint.x ? 1 : -1;
        hoop.Rotate(sign * aimingAngle);

        // calculate net scale
        float netScaleY = Mathf.Min(netMaxElongation, Mathf.Max(1f, 1f + distance / 5f));
        hoop.ScaleNet(netScaleY);

        // trajectory
        if (canShoot)
        {
            if (!trajectory.IsShowing())
                trajectory.Show();

            trajectory.Simulate(ObjectPooler.Instance.GetPrefab(ObjectTag.Ball), ball.transform.position, force);
        }
        else
        {
            if (trajectory.IsShowing())
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
            hoop.ShootBall();
            active = false;
        }
        else
        {
            hoop.CancelShoot();
        }
    }

    private void OnDestroy()
    {
        ball.Renew();
        ObjectPooler.Instance.Recall(ball.gameObject);
    }
}