using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : UIGame
{
    [SerializeField] private GameObject _skinPage, _themePage;
    [SerializeField] private Button _skinButton, _themeButton;
    [SerializeField] private Color _activeButtonColor, _deactiveButtonColor;

    private void OnEnable()
    {
        ActiveSkinPage();
    }

    private void ActiveSkinPage()
    {
        _skinPage.SetActive(true);
        _skinButton.interactable = false;
        _skinButton.GetComponent<Image>().color = _activeButtonColor;

        _themePage.SetActive(false);
        _themeButton.interactable = true;
        _themeButton.GetComponent<Image>().color = _deactiveButtonColor;

        MoneyManager.Instance.ShowUIStar();
    }

    private void ActiveThemePage()
    {
        _skinPage.SetActive(false);
        _skinButton.interactable = true;
        _skinButton.GetComponent<Image>().color = _deactiveButtonColor;

        _themePage.SetActive(true);
        _themeButton.interactable = false;
        _themeButton.GetComponent<Image>().color = _activeButtonColor;

        MoneyManager.Instance.ShowUIToken();
    }

    public void OnBackButton()
    {
        UIManager.Instance.CloseCustomize();
    }

    public void OnVideoButton()
    {
        Debug.Log("ADS");
        MoneyManager.Instance.AddStar(25);
    }

    public void OnSpinButton()
    {
        Debug.Log("SPIN");
        MoneyManager.Instance.AddToken(25);
    }

    public void OnSkinButton()
    {
        ActiveSkinPage();
    }

    public void OnThemeButton()
    {
        ActiveThemePage();
    }
}