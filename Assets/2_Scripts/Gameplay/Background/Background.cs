using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background Instance { get; private set; }

    [SerializeField] private SpriteRenderer _BG;
    [SerializeField] private ParallaxScrollBG _wallBG;
    [SerializeField] private ParallaxScrollBG _staticWallBG;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadTheme();

        Observer.ChangeTheme += LoadTheme;
        Observer.OnLightMode += OnLightMode;
        Observer.OnDarkMode += OnDarkMode;
    }

    private void LoadTheme()
    {
        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
        {
            OnLightMode();
        }
        else
        {
            OnDarkMode();
        }
    }

    private void OnLightMode()
    {
        _BG.sprite = DataManager.Instance.ThemeInUse.Data.Background.LightBG;
    }

    private void OnDarkMode()
    {
        _BG.sprite = DataManager.Instance.ThemeInUse.Data.Background.DarkBG;
    }

    public void Renew()
    {
        _wallBG.Renew();
    }
}