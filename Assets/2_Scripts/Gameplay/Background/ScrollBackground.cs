using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float speedFactor;

    private Transform[] _listImage;
    private float _imageDistance;
    private int _visibleImg;

    private Transform mainCam;
    private Vector3 camPos, targetPos;
    private float distance;

    private void Awake()
    {
        mainCam = Camera.main.transform;
        _imageDistance = mainCam.GetComponent<Camera>().orthographicSize * 2f;

        _listImage = new Transform[transform.childCount];
        for (int i = 0; i < _listImage.Length; i++)
            _listImage[i] = transform.GetChild(i);
    }

    private void FixedUpdate()
    {
        distance = (mainCam.position.y - camPos.y) / Time.fixedDeltaTime;
        camPos = mainCam.position;

        foreach (var img in _listImage)
        {
            targetPos = img.position - new Vector3(0f, distance * speedFactor);
            img.position = Vector3.Lerp(img.position, targetPos, Time.fixedDeltaTime);
        }

        if (_listImage[1 - _visibleImg].position.y <= mainCam.transform.position.y)
        {
            _listImage[_visibleImg].transform.position = _listImage[1 - _visibleImg].transform.position + new Vector3(0f, _imageDistance);
            _visibleImg = 1 - _visibleImg;
        }
    }

    public void Renew()
    {
        // set lại vị trí cho các image
        for (int i = 0; i < _listImage.Length; i++)
            _listImage[i].position = _imageDistance * i * Vector3.up;

        _visibleImg = 0;
        camPos = mainCam.transform.position;
    }
}