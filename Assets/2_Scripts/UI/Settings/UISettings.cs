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
        Application.OpenURL("https://play.google.com/store/apps/dev?id=7079675741327974212");
    }

    public void OnFacebookButton()
    {
        Debug.Log("FACEBOOK");
        Application.OpenURL("https://www.facebook.com/NgTienHungg/");
    }

    public void OnInstagramButton()
    {
        Debug.Log("INSTAGRAM");
    }
}