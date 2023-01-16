using UnityEngine;
using DG.Tweening;

public class UIMainMenu : UIGame
{
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