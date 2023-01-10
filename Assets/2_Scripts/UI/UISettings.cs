using DG.Tweening;
using UnityEngine;

public class UISettings : UIGame
{
    [SerializeField] private RectTransform adsButton;

    private void OnEnable()
    {
        adsButton.transform.DOKill();
    }

    public override void Enable()
    {
        base.Enable();

        adsButton.DORotate(Vector3.forward * 10f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    public void OnBackButton()
    {
        UIManager.Instance.OnCloseSettings();
    }

    public void OnSoundsButton()
    {
    }

    public void OnVibrationButton()
    {
    }

    public void OnNightModeButton()
    {
    }

    public void OnRemoveAdsButton()
    {
    }

    public void OnPrivacyInfoButton()
    {
    }

    public void OnRestorePurchaseButton()
    {
    }

    public void OnAdsButton()
    {
    }

    public void OnFacebookButton()
    {
    }

    public void OnInstagramButton()
    {
    }
}