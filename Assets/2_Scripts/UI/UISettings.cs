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
        adsButton.DORotate(Vector3.forward * 10f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnBackButton()
    {
        // audio
        UIManager.Instance.OnCloseSettings();
    }

    public void OnSoundsButton()
    {
        // audio
    }

    public void OnVibrationButton()
    {
        // audio
    }

    public void OnNightModeButton()
    {
        // audio
    }

    public void OnRemoveAdsButton()
    {
        // audio
    }

    public void OnPrivacyInfoButton()
    {
        // audio
    }

    public void OnRestorePurchaseButton()
    {
        // audio
    }

    public void OnAdsButton()
    {
        // audio
    }

    public void OnFacebookButton()
    {
        // audio
    }

    public void OnInstagramButton()
    {
        // audio
    }
}