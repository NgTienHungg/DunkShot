using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private Hoop hoop;
    [SerializeField] private Trajectory trajectory;

    [SerializeField] private float netMaxElongation = 1.85f; // độ giãn tối đa của lưới
    [SerializeField] private float pushForce, maxDistance;

    private bool isAiming;
    public Vector3 pivot, startPoint, endPoint;
    public Vector3 direction, force;
    public float distance;

    private void Start()
    {
        this.isAiming = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAiming)
            StartAiming();

        if (Input.GetMouseButtonUp(0) && isAiming)
            Shot();

        if (isAiming)
            Aiming();
    }

    private void StartAiming()
    {
        pivot = hoop.transform.position;
        startPoint = Util.GetMouseWorldPosition();

        ball.Stop();
        trajectory.Show();

        this.isAiming = true;
    }

    private void Aiming()
    {
        endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Mathf.Min(maxDistance, Vector3.Distance(startPoint, endPoint));
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        // trajectory
        trajectory.UpdateDots(ball.transform.position, force);
        Debug.DrawLine(startPoint, endPoint, Color.red);

        // calculate angle of hoop
        float aimingAngle = Vector3.Angle(force, Vector3.up);
        float sign = endPoint.x > startPoint.x ? 1 : -1;
        hoop.transform.eulerAngles = new Vector3(0f, 0f, sign * aimingAngle);

        // calculate net scale
        float dy = Mathf.Abs(startPoint.y - endPoint.y);
        float netScaleY = Mathf.Min(netMaxElongation, Mathf.Max(1f, 1f + dy / 5f));
        hoop.Net.transform.localScale = new Vector3(1f, netScaleY, 1f);
    }

    private void Shot()
    {
        isAiming = false;

        trajectory.Hide();
        ball.Push(force);
        hoop.Net.Elastic();
    }
}