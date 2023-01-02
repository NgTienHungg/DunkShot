using DG.Tweening;

public class UIPaused : UIGame
{
    protected override void OnEnable()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1f;
    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
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
        gameObject.SetActive(false);
    }

    public void OnSettingsButton()
    {
        // audio
        UIManager.Instance.OnSettings();
    }

    public void OnMainMenuButton()
    {
        // audio
        UIManager.Instance.OnBackHome();
    }

    public void OnCustomizeButton()
    {
        // audio
    }

    public void OnLeaderboardButton()
    {
        // audio
    }
    public void OnResumeButton()
    {
        // audio
        Controller.Instance.Resume();
    }
}