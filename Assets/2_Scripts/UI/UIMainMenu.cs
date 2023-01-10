using UnityEngine;
using DG.Tweening;

public class UIMainMenu : UIGame
{
    public override void Disable()
    {
        _canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _canvasGroup.alpha = 1f;
            gameObject.SetActive(false);
        });
    }

    public void OnCustomizeButton()
    {
        UIManager.Instance.OpenCustomize();
    }

    public void OnChallengesButton()
    {
        UIManager.Instance.OpenChallenge();
    }

    public void OnSettingsButton()
    {
        UIManager.Instance.OnSettings();
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