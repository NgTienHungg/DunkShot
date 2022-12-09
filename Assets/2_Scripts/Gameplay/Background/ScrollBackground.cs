using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float speedFactor;

    private Transform[] images;
    private float imageDistance;
    private int visibleImg;

    private Transform mainCam;
    private Vector3 camPos, targetPos;
    private float distance;

    private void Awake()
    {
        mainCam = Camera.main.transform;
        imageDistance = mainCam.GetComponent<Camera>().orthographicSize * 2f;

        images = new Transform[transform.childCount];
        for (int i = 0; i < images.Length; i++)
            images[i] = transform.GetChild(i);
    }

    private void FixedUpdate()
    {
        distance = (mainCam.position.y - camPos.y) / Time.fixedDeltaTime;
        camPos = mainCam.position;

        foreach (var img in images)
        {
            targetPos = img.position - new Vector3(0f, distance * speedFactor);
            img.position = Vector3.Lerp(img.position, targetPos, Time.fixedDeltaTime);
        }

        if (images[1 - visibleImg].position.y <= mainCam.transform.position.y)
        {
            images[visibleImg].transform.position = images[1 - visibleImg].transform.position + new Vector3(0f, imageDistance);
            visibleImg = 1 - visibleImg;
        }
    }

    public void Renew()
    {
        // set lại vị trí cho các image
        for (int i = 0; i < images.Length; i++)
            images[i].position = Vector3.up * i * imageDistance;

        visibleImg = 0;
        camPos = mainCam.transform.position;
    }
}