using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _bound;
    [SerializeField] private GameObject _vCamPrefab;

    private Camera _mainCam;
    private CinemachineVirtualCamera _vCam;
    private Vector3 _startPos = new Vector3(0f, 0f, -10f);

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public void SetupCamera()
    {
        if (_vCam != null)
        {
            Destroy(_vCam.gameObject);
        }

        _vCam = Instantiate(_vCamPrefab).GetComponent<CinemachineVirtualCamera>();
        _vCam.transform.parent = transform.parent;

        _mainCam.transform.position = _startPos;
        _mainCam.orthographicSize = _vCam.m_Lens.OrthographicSize = _bound.bounds.size.x * Screen.height / Screen.width * 0.5f;
    }

    public void FollowBall()
    {
        _vCam.Follow = GameController.Instance.Mechanic.Ball.transform;
    }

    public void UnfollowBall()
    {
        _vCam.Follow = null;
    }
}