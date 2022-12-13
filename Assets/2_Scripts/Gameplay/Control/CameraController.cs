using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private GameObject cinemachinePrefab;

    private Camera mainCamera;
    private CinemachineVirtualCamera cinemachine;
    private Vector3 startPos = new Vector3(0f, 0f, -10f);

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    public void FollowBall()
    {
        cinemachine.Follow = Controller.Instance.Mechanic.GetBall().transform;
    }

    public void UnfollowBall()
    {
        cinemachine.Follow = null;
    }

    public void Renew()
    {
        if (cinemachine != null)
        {
            Destroy(cinemachine.gameObject);
        }

        cinemachine = Instantiate(cinemachinePrefab).GetComponent<CinemachineVirtualCamera>();
        cinemachine.transform.parent = transform.parent;

        mainCamera.transform.position = startPos;
    }
}