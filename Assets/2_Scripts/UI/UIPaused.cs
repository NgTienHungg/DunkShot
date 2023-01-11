using DG.Tweening;
using UnityEngine;

public class UIPaused : UIGame
{
    [SerializeField] private Transform _resumeButton;

    private void OnEnable()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1f;

        _resumeButton.localScale = Vector3.one;
        _resumeButton.DOScale(0.96f, 1f).SetEase(Ease.InOutQuad).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
    }

    public override void Disable()
    {
        _resumeButton.DOKill();
        _canvasGroup.interactable = false;
        _canvasGroup.DOFade(0f, 0.6f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public override void DisableImmediately()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;
        _resumeButton.DOKill();
        gameObject.SetActive(false);
    }

    public void OnSettingsButton()
    {
        CanvasController.Instance.OnSettings();
    }

    public void OnMainMenuButton()
    {
        CanvasController.Instance.OnBackHome();
    }

    public void OnCustomizeButton()
    {
        CanvasController.Instance.OpenCustomize();
    }

    public void OnLeaderboardButton()
    {
        Debug.Log("LEADERBOARD");
    }

    public void OnResumeButton()
    {
        Controller.Instance.Resume();
    }
}