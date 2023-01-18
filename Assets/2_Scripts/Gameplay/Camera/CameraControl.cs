using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _bound;
    [SerializeField] private GameObject _vCamPrefab;

    [Header("Shake")]
    [SerializeField] private float _intensity;
    [SerializeField] private float _shakeDuration;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private float _shakeTimer;
    private float _currentIntensity;

    private Camera _mainCam;
    private CinemachineVirtualCamera _vCam;
    private Vector3 _startPos = new Vector3(0f, 0f, -10f);

    private void Awake()
    {
        _mainCam = Camera.main;
        Observer.BallFlame += ShakeCamera;
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
        _vCam.Follow = GameController.Instance.BallShooting.Ball.transform;
    }

    public void UnfollowBall()
    {
        _vCam.Follow = null;
    }

    private void ShakeCamera()
    {
        AudioManager.Instance.PlayVibrate();
        _shakeTimer = _shakeDuration;
        _currentIntensity = _intensity;
        _cinemachineBasicMultiChannelPerlin = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _currentIntensity;
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            _currentIntensity -= 0.02f;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _currentIntensity;

            if (_shakeTimer <= 0)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}