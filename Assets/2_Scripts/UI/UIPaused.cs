using DG.Tweening;

public class UIPaused : UIGame
{
    private void OnEnable()
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }

    public override void Enable()
    {

    }

    public override void Disable()
    {
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0f, 0.6f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void DisableImmediate()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0f;
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