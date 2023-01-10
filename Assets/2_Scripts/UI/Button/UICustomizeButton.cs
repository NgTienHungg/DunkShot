using UnityEngine;
using UnityEngine.UI;

public class UICustomizeButton : MonoBehaviour
{
    [SerializeField] private Image _progress;

    private int _totalSkin, _count;

    private void Awake()
    {
        _totalSkin = DataManager.Instance.Skins.Length + DataManager.Instance.Themes.Length;

        Reload();

        Observer.OnUnlockSkin += OnUnlockSkin;
        Observer.OnUnlockTheme += OnUnlockTheme;
    }

    private void OnUnlockSkin()
    {
        Reload();
    }

    private void OnUnlockTheme()
    {
        Reload();
    }

    private void Reload()
    {
        _count = 0;

        foreach (var skin in DataManager.Instance.Skins)
        {
            if (skin.Unlocked)
            {
                _count++;
            }
        }

        foreach (var theme in DataManager.Instance.Themes)
        {
            if (theme.Unlocked)
            {
                _count++;
            }
        }

        _progress.fillAmount = 1f * _count / _totalSkin;
    }
}