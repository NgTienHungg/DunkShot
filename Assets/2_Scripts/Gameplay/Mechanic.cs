using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject hoop;

    private Rigidbody2D ballRigidBody;

    private Vector3 pivotPos; // vị trí click chuột đầu tiên
    private Vector3 mouseOffset; 

    private bool isDragging = false;

    private void Awake()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            Debug.Log("dragging");
            isDragging = true;
            ballRigidBody.bodyType = RigidbodyType2D.Static;
            mouseOffset = ball.transform.localPosition - Util.GetMouseWorldPosition();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Debug.Log("realse");
            isDragging = false;
            ballRigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            ball.transform.localPosition = Util.GetMouseWorldPosition() + mouseOffset;
        }
    }
}