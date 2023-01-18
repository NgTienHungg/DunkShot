using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Image _progress;

    private int _total, _owned;

    private void Awake()
    {
        _total = DataManager.Instance.Skins.Length + DataManager.Instance.Themes.Length;
    }

    private void OnEnable()
    {
        _owned = 0;

        foreach (var skin in DataManager.Instance.Skins)
        {
            if (skin.Unlocked)
            {
                _owned++;
            }
        }

        foreach (var theme in DataManager.Instance.Themes)
        {
            if (theme.Unlocked)
            {
                _owned++;
            }
        }

        _progress.fillAmount = 1f * _owned / _total;

        //Reload();

        //Observer.OnUnlockSkin += OnUnlockSkin;
        //Observer.OnUnlockTheme += OnUnlockTheme;
    }

    //private void OnUnlockSkin()
    //{
    //    Reload();
    //}

    //private void OnUnlockTheme()
    //{
    //    Reload();
    //}

    //private void Reload()
    //{
    //    _count = 0;

    //    foreach (var skin in DataManager.Instance.Skins)
    //    {
    //        if (skin.Unlocked)
    //        {
    //            _count++;
    //        }
    //    }

    //    foreach (var theme in DataManager.Instance.Themes)
    //    {
    //        if (theme.Unlocked)
    //        {
    //            _count++;
    //        }
    //    }

    //    _progress.fillAmount = 1f * _count / _totalSkin;
    //}
}