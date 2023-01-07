using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float speedFactor;

    private Transform[] _images;
    private float _imageDistance;
    private int _visibleImage;

    private Transform mainCam;
    private Vector3 camPos, targetPos;
    private float velocity;

    private void Awake()
    {
        mainCam = Camera.main.transform;
        _imageDistance = mainCam.GetComponent<Camera>().orthographicSize * 2f;

        _images = new Transform[transform.childCount];
        for (int i = 0; i < _images.Length; i++)
            _images[i] = transform.GetChild(i);
    }

    private void FixedUpdate()
    {
        velocity = (mainCam.position.y - camPos.y) / Time.fixedDeltaTime;
        camPos = mainCam.position;

        foreach (var img in _images)
        {
            targetPos = img.position - new Vector3(0f, velocity * speedFactor);
            img.position = Vector3.Lerp(img.position, targetPos, Time.fixedDeltaTime);
        }

        if (_images[1 - _visibleImage].position.y <= mainCam.transform.position.y)
        {
            _images[_visibleImage].transform.position = _images[1 - _visibleImage].transform.position + new Vector3(0f, _imageDistance);
            _visibleImage = 1 - _visibleImage;
        }
    }

    public void Renew()
    {
        // set lại vị trí cho các image
        for (int i = 0; i < _images.Length; i++)
            _images[i].position = _imageDistance * i * Vector3.up;

        _visibleImage = 0;
        camPos = mainCam.transform.position;
    }
}