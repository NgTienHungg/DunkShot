using DG.Tweening;
using UnityEngine;

public class UIGameOver : UIGame
{
    [SerializeField] private RectTransform newBallButton, videoButton;
    [SerializeField] private RectTransform rateButton, restartButton, setttingsButton;

    private void OnEnable()
    {
        newBallButton.localScale = Vector3.zero;

        videoButton.localScale = Vector3.zero;
        videoButton.transform.DOKill();

        rateButton.localScale = Vector3.zero;

        restartButton.localScale = Vector3.zero;

        setttingsButton.localScale = Vector3.zero;
    }

    public override void Enable()
    {
        newBallButton.DOScale(1f, 0.4f).SetEase(Ease.OutQuint);

        videoButton.DOScale(1.15f, 0.4f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            videoButton.DOScale(1f, 0.8f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });

        rateButton.DOScale(1f, 0.4f).SetEase(Ease.OutQuint).SetDelay(0.15f);

        restartButton.DOScale(1f, 0.4f).SetEase(Ease.OutQuint).SetDelay(0.25f);

        setttingsButton.DOScale(1f, 0.4f).SetEase(Ease.OutQuint).SetDelay(0.35f);
    }

    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnNewBallButton()
    {
        // audio
    }

    public void OnVideoAdsButton()
    {
        // audio
    }

    public void OnRateButton()
    {
        // audio
    }

    public void OnRestartButton()
    {
        // audio
        UIManager.Instance.OnBackHome();
    }

    public void OnSettingsButton()
    {
        // audio
    }
}