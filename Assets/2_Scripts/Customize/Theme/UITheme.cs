using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITheme : MonoBehaviour
{
    [SerializeField] private GameObject _selected;
    [SerializeField] private GameObject _locked;
    [SerializeField] private Image _themeCell;
    [SerializeField] private TextMeshProUGUI _price;

    private Theme _theme;
    private bool _isSelecting;

    private void Awake()
    {
        _selected.SetActive(false);
        _locked.SetActive(true);
    }

    public void Renew()
    {
        _isSelecting = _theme.Key == DataManager.Instance.ThemeInUse.Key ? true : false;

        if (_isSelecting)
        {
            _selected.SetActive(true);
        }
    }

    public void SetTheme(Theme theme)
    {
        _theme = theme;
        _themeCell.sprite = _theme.Data.ThemeCell;
        _price.text = _theme.Data.Price.ToString();

        if (_theme.Unlocked)
        {
            _locked.SetActive(false);
        }
    }

    public void OnClick()
    {
    }
}