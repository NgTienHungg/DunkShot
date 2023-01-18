using TMPro;
using DG.Tweening;
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
        _isSelecting = (_theme.Key == DataManager.Instance.ThemeInUse.Key) ? true : false;

        if (_isSelecting)
        {
            _selected.SetActive(true);
            _selected.transform.DOScale(0.8f, 0f).SetUpdate(true).OnComplete(() =>
            {
                _selected.transform.DOScale(1f, 0.3f).SetEase(Ease.OutCubic).SetUpdate(true);
            });
        }
        else
        {
            _selected.SetActive(false);
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
        if (_isSelecting)
        {
            CanvasController.Instance.CloseCustomize();
            return;
        }

        if (!_theme.Unlocked)
        {
            if (!MoneyManager.Instance.CanBuyByToken(_theme.Data.Price))
            {
                Debug.Log("NOT ENOUGH");
                AudioManager.Instance.PlaySound(AudioKey.POPUP_APPEAR);
                Observer.OnShowThemePopup?.Invoke(_theme);
                return;
            }
            Unlock();
        }
        Select();
    }

    private void Unlock()
    {
        AudioManager.Instance.PlaySound(AudioKey.SHOP_BUY);
        MoneyManager.Instance.BuyByToken(_theme.Data.Price);
        _theme.Unlock();
        _locked.SetActive(false);
    }

    private void Select()
    {
        AudioManager.Instance.PlaySound(AudioKey.SHOP_SELECT);
        _theme.Select();
        Observer.OnChangeTheme?.Invoke();
    }
}