using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background Instance { get; private set; }

    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _static;
    [SerializeField] private ParallaxScrollBG _wall;
    [SerializeField] private ParallaxScrollBG _foreground;
    [SerializeField] private SpriteRenderer _decor;

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
        _background.sprite = DataManager.Instance.ThemeInUse.Data.Background.LightBG;
        _static.sprite = DataManager.Instance.ThemeInUse.Data.Background.LightStaticBG;
        _decor.sprite = DataManager.Instance.ThemeInUse.Data.Background.LightDecor;
    }

    private void OnDarkMode()
    {
        _background.sprite = DataManager.Instance.ThemeInUse.Data.Background.DarkBG;
        _static.sprite = DataManager.Instance.ThemeInUse.Data.Background.DarkStaticBG;
        _decor.sprite = DataManager.Instance.ThemeInUse.Data.Background.DarkDecor;
    }

    public void Renew()
    {
        _wall.Renew();
    }
}