using UnityEngine;

public enum BGType
{
    Wall,
    Foreground
}

public class ParallaxScrollBG : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _speedFactor;

    [SerializeField] private BGType _type;

    private SpriteRenderer[] _images;
    private readonly float _distance = 20.5f;
    private int _visible = 0;

    private Transform mainCam;
    private Vector3 camPos, targetPos;

    private void Awake()
    {
        mainCam = Camera.main.transform;

        _images = new SpriteRenderer[transform.childCount];

        for (int i = 0; i < _images.Length; i++)
        {
            _images[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        LoadTheme();

        Observer.ChangeTheme += LoadTheme;
        Observer.OnLightMode += LightMode;
        Observer.OnDarkMode += DarkMode;
    }

    private void LoadTheme()
    {
        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
        {
            LightMode();
        }
        else
        {
            DarkMode();
        }
    }

    private void LightMode()
    {
        if (_type == BGType.Wall)
        {
            _speedFactor = DataManager.Instance.ThemeInUse.Data.Background.WallSpeed;

            foreach (var img in _images)
            {
                img.sprite = DataManager.Instance.ThemeInUse.Data.Background.LightWall;
            }
        }
        else if (_type == BGType.Foreground)
        {
            _speedFactor = DataManager.Instance.ThemeInUse.Data.Background.ForegroundSpeed;

            foreach (var img in _images)
            {
                img.sprite = DataManager.Instance.ThemeInUse.Data.Background.LightForeground;
            }
        }
    }

    private void DarkMode()
    {
        if (_type == BGType.Wall)
        {
            _speedFactor = DataManager.Instance.ThemeInUse.Data.Background.WallSpeed;

            foreach (var img in _images)
            {
                img.sprite = DataManager.Instance.ThemeInUse.Data.Background.DarkWall;
            }
        }
        else if (_type == BGType.Foreground)
        {
            _speedFactor = DataManager.Instance.ThemeInUse.Data.Background.ForegroundSpeed;

            foreach (var img in _images)
            {
                img.sprite = DataManager.Instance.ThemeInUse.Data.Background.DarkForeground;
            }
        }
    }

    private void FixedUpdate()
    {
        float vel = (mainCam.position.y - camPos.y) / Time.fixedDeltaTime;
        camPos = mainCam.position;

        foreach (var img in _images)
        {
            targetPos = img.transform.position - new Vector3(0f, vel * _speedFactor);
            img.transform.position = Vector3.Lerp(img.transform.position, targetPos, Time.fixedDeltaTime);
        }

        if (_images[1 - _visible].transform.position.y <= mainCam.transform.position.y)
        {
            _images[_visible].transform.position = _images[1 - _visible].transform.position + new Vector3(0f, _distance);
            _visible = 1 - _visible;
        }
    }

    public void Renew()
    {
        // set lại vị trí cho các image
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].transform.position = _distance * i * Vector3.up;
        }

        _visible = 0;
        camPos = mainCam.transform.position;
    }
}