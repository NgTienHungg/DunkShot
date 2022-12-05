using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private Camera main;
    [SerializeField] private CinemachineVirtualCamera cinemachine;
    [SerializeField] private CinemachineBrain brain;

    private Vector3 startPos = new Vector3(0f, 0f, -10f);

    private void Awake()
    {
        Instance = this;
    }

    public void FollowBall(Transform ball)
    {
        if (cinemachine.Follow == null)
        {
            cinemachine.Follow = ball;
            brain.enabled = true;
        }
    }

    public void UnfollowBall()
    {
        cinemachine.Follow = null;
    }

    public void Renew()
    {
        Debug.Log("renew cam");

        main.transform.position = startPos;
        cinemachine.transform.position = startPos;

        cinemachine.Follow = null;
        brain.enabled = false;
    }
}