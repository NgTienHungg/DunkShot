using DG.Tweening;

public class UIMainMenu : UIGame
{
    private void OnEnable()
    {
    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
        canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            canvasGroup.alpha = 1f;
            gameObject.SetActive(false);
        });
    }

    public void OnSettingsButton()
    {
        // audio
        UIManager.Instance.OnSettings();
    }

    public void OnLeaderboardButton()
    {
        // audio
    }

    public void OnLightButton()
    {
        // audio
    }

    public void OnGiftButton()
    {
        // audio
    }

    public void OnCustomizeButton()
    {
        // audio
    }

    public void OnChallengesButton()
    {
        // audio
    }
}