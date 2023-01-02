using DG.Tweening;

public class UIMainMenu : UIGame
{
    protected override void OnEnable()
    {
    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
        _canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _canvasGroup.alpha = 1f;
            gameObject.SetActive(false);
        });
    }

    public override void DisableImmediately()
    {
    }

    public void OnCustomizeButton()
    {
        // audio
        UIManager.Instance.OpenCustomize();
    }

    public void OnChallengesButton()
    {
        // audio
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
}