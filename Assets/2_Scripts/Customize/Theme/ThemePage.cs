using UnityEngine;
using System.Collections.Generic;

public class ThemePage : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private GameObject _uiThemePrefab;
    [SerializeField] private Transform _normalThemeContent;

    [Header("Season")]
    [SerializeField] private Transform _seasonThemeContent;

    private List<UITheme> _listUiTheme;

    private void Awake()
    {
        _listUiTheme = new List<UITheme>();

        foreach (var theme in DataManager.Instance.Themes)
        {
            _listUiTheme.Add(CreateUITheme(theme));
        }
    }

    private UITheme CreateUITheme(Theme theme)
    {
        UITheme uiTheme = null;

        switch (theme.Data.Type)
        {
            case ThemeType.Normal:
                uiTheme = Instantiate(_uiThemePrefab, _normalThemeContent).GetComponent<UITheme>();
                break;
            case ThemeType.Season:
                uiTheme = Instantiate(_uiThemePrefab, _seasonThemeContent).GetComponent<UITheme>();
                break;
        }

        uiTheme.name = theme.Key;
        uiTheme.SetTheme(theme);
        return uiTheme;
    }

    private void OnEnable()
    {
        ReloadPage();

        Observer.ChangeTheme += ReloadPage;
    }

    private void OnDisable()
    {
        Observer.ChangeTheme -= ReloadPage;
    }

    private void ReloadPage()
    {
        foreach (var uiSkin in _listUiTheme)
        {
            uiSkin.Renew();
        }
    }
}