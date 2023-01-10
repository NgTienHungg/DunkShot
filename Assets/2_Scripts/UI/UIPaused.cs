using DG.Tweening;

public class UIPaused : UIGame
{
    private void OnEnable()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1f;
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
        UIManager.Instance.OnSettings();
    }

    public void OnMainMenuButton()
    {
        UIManager.Instance.OnBackHome();
    }

    public void OnCustomizeButton()
    {
        UIManager.Instance.OpenCustomize();
    }

    public void OnLeaderboardButton()
    {
    }

    public void OnResumeButton()
    {
        Controller.Instance.Resume();
    }
}