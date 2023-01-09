using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : UIGame
{
    [SerializeField] private GameObject _skinPage, _themePage;
    [SerializeField] private Button _skinButton, _themeButton;
    [SerializeField] private Color _activeButtonColor, _deactiveButtonColor;

    protected override void OnEnable()
    {
        ActiveThemePage();
    }

    public override void Enable()
    {
        gameObject.SetActive(true);
    }

    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public override void DisableImmediately()
    {
        gameObject.SetActive(false);
    }

    private void ActiveSkinPage()
    {
        _skinPage.SetActive(true);
        _skinButton.interactable = false;
        _skinButton.GetComponent<Image>().color = _activeButtonColor;

        _themePage.SetActive(false);
        _themeButton.interactable = true;
        _themeButton.GetComponent<Image>().color = _deactiveButtonColor;
    }

    private void ActiveThemePage()
    {
        _skinPage.SetActive(false);
        _skinButton.interactable = true;
        _skinButton.GetComponent<Image>().color = _deactiveButtonColor;

        _themePage.SetActive(true);
        _themeButton.interactable = false;
        _themeButton.GetComponent<Image>().color = _activeButtonColor;
    }

    public void OnBackButton()
    {
        // audio
        UIManager.Instance.CloseCustomize();
    }

    public void OnVideoButton()
    {
        // audio
        Debug.Log("ADS");
    }

    public void OnSkinButton()
    {
        // audio
        ActiveSkinPage();
    }

    public void OnThemeButton()
    {
        // audio
        ActiveThemePage();
    }
}