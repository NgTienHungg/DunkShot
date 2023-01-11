using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIThemeControl : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private Color _lightPanelColor;
    [SerializeField] private Color _darkPanelColor;
    [SerializeField] private Image[] _panels;

    [Header("UI")]
    [SerializeField] private Image[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _texts;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _score;

    private void Awake()
    {
        ApplyTheme();
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnChangeTheme += ApplyTheme;
        Observer.OnLightMode += ApplyLightMode;
        Observer.OnDarkMode += ApplyDarkMode;
    }

    private void ApplyTheme()
    {
        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
        {
            ApplyLightMode();
        }
        else
        {
            ApplyDarkMode();
        }
    }

    private void ApplyLightMode()
    {
        foreach (var image in _panels)
        {
            image.color = _lightPanelColor;
        }

        _score.color = DataManager.Instance.ThemeInUse.Data.Color.LightScore;

        foreach (var image in _buttons)
        {
            image.color = DataManager.Instance.ThemeInUse.Data.Color.LightUI;
        }

        foreach (var text in _texts)
        {
            text.color = DataManager.Instance.ThemeInUse.Data.Color.LightUI;
        }
    }

    private void ApplyDarkMode()
    {
        foreach (var image in _panels)
        {
            image.color = _darkPanelColor;
        }

        _score.color = DataManager.Instance.ThemeInUse.Data.Color.DarkScore;

        foreach (var image in _buttons)
        {
            image.color = DataManager.Instance.ThemeInUse.Data.Color.DarkUI;
        }

        foreach (var text in _texts)
        {
            text.color = DataManager.Instance.ThemeInUse.Data.Color.DarkUI;
        }
    }
}