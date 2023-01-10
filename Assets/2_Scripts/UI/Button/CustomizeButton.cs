using UnityEngine;
using UnityEngine.UI;

public class CustomizeButton : MonoBehaviour
{
    [SerializeField] private Image _progress;

    private int _totalSkin, _count;

    private void Awake()
    {
        _totalSkin = DataManager.Instance.Skins.Length;

        Reload();

        Observer.OnUnlockSkin += OnUnlockSkin;
    }

    private void OnUnlockSkin(Skin skin)
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

        _progress.fillAmount = 1f * _count / _totalSkin;
    }
}