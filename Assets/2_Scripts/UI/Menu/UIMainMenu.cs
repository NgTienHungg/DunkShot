using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIMainMenu : UIGame
{
    [SerializeField] private Image _gameTitle;
    [SerializeField] private Image _settings, _leadboard, _light, _tutorial;
    [SerializeField] private Image _giftButton, _shopButton, _challengeButton;

    public void OpenApp()
    {
        gameObject.SetActive(true);
        _canvasGroup.interactable = false;

        _gameTitle.DOFade(0f, 0f);
        _gameTitle.rectTransform.DOAnchorPosY(-700f, 0f);

        _settings.DOFade(0f, 0f);
        _leadboard.DOFade(0f, 0f);
        _light.DOFade(0f, 0f);
        _tutorial.DOFade(0f, 0f);

        _giftButton.transform.localScale = Vector3.zero;
        _shopButton.transform.localScale = Vector3.zero;
        _challengeButton.transform.localScale = Vector3.zero;

        _gameTitle.DOFade(1f, 0.8f).OnComplete(() =>
        {
            _gameTitle.rectTransform.DOAnchorPosY(-280f, 1.2f).SetEase(Ease.InQuint).SetDelay(0.2f).OnComplete(() =>
            {
                _settings.DOFade(1f, 0.5f);
                _leadboard.DOFade(1f, 0.5f);
                _light.DOFade(1f, 0.5f);
                _tutorial.DOFade(1f, 0.5f);

                _giftButton.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
                _shopButton.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack).SetDelay(0.06f);
                _challengeButton.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack).SetDelay(0.12f).OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                    GameController.Instance.OpenApp();
                });
            });
        });
    }

    public override void Enable()
    {
        base.Enable();
        _canvasGroup.alpha = 1f;
        _canvasGroup.interactable = true;
    }

    public override void Disable()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void OnCustomizeButton()
    {
        CanvasController.Instance.OpenCustomize();
    }

    public void OnChallengesButton()
    {
        CanvasController.Instance.OpenChallenge();
    }

    public void OnSettingsButton()
    {
        CanvasController.Instance.OpenSettings();
    }

    public void OnLeaderboardButton()
    {
        Debug.Log("LEADERBOARD");
    }

    public void OnGiftButton()
    {
        Debug.Log("GIFT");
    }
}