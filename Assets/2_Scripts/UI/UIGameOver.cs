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
        newBallButton.DOScale(1f, 0.5f).SetEase(Ease.OutQuint);

        videoButton.DOScale(1f, 0.5f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            videoButton.DOScale(0.85f, 0.8f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });

        rateButton.DOScale(1f, 0.5f).SetEase(Ease.OutQuint).SetDelay(0.2f);

        restartButton.DOScale(1f, 0.5f).SetEase(Ease.OutQuint).SetDelay(0.35f);

        setttingsButton.DOScale(1f, 0.5f).SetEase(Ease.OutQuint).SetDelay(0.50f);
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