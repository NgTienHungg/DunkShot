using DG.Tweening;
using UnityEngine;

public class UISettings : UIGame
{
    [SerializeField] private RectTransform adsButton;

    private void OnEnable()
    {
        adsButton.DOKill();
    }

    public override void Enable()
    {
        base.Enable();

        adsButton.DORotate(Vector3.forward * 10f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }

    public void OnBackButton()
    {
        CanvasController.Instance.CloseSettings();
    }

    public void OnSoundButton()
    {
    }

    public void OnVibrationButton()
    {
    }

    public void OnDarkModeButton()
    {
        Debug.Log("FACEBOOK");
    }

    public void OnRemoveAdsButton()
    {
        Debug.Log("REMOVE ADS");
    }

    public void OnPrivacyInfoButton()
    {
        Debug.Log("PRIVACY INFO");
    }

    public void OnRestorePurchaseButton()
    {
        Debug.Log("RESTORE PURCHASE");
    }

    public void OnAdsButton()
    {
        Debug.Log("ADS");
    }

    public void OnFacebookButton()
    {
        Debug.Log("FACEBOOK");
    }

    public void OnInstagramButton()
    {
        Debug.Log("INSTAGRAM");
    }
}