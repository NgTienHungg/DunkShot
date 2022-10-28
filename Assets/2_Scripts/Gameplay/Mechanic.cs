using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject hoop;

    private Rigidbody2D ballRigidBody;

    public Vector3 pivotPosition, touchPosition;
    public float originalAngle;

    private bool isAiming;

    private void Awake()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.isAiming = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAiming)
            StartAiming();

        if (isAiming)
            Aiming();

        if (Input.GetMouseButtonUp(0) && isAiming)
            Shot();
    }

    private void StartAiming()
    {
        this.isAiming = true;

        pivotPosition = hoop.transform.position;
        touchPosition = Util.GetMouseWorldPosition();
        originalAngle = Util.CalculateAngleDeg(pivotPosition, touchPosition);
    }

    private void Aiming()
    {
        Vector3 mousePosition = Util.GetMouseWorldPosition();
        float currentAngle = Util.CalculateAngleDeg(pivotPosition, mousePosition);
        float deltaAngle = currentAngle - originalAngle;

        Debug.Log("Current angle: " + currentAngle + " - Delta angle: " + deltaAngle);
        hoop.transform.eulerAngles = new Vector3(0f, 0f, deltaAngle);
    }

    private void Shot()
    {
        isAiming = false;
        ballRigidBody.bodyType = RigidbodyType2D.Dynamic;
    }
}