using DG.Tweening;

public class UIGamePlay : UIGame
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

    public void OnPauseButton()
    {
        // audio
        Controller.Instance.Pause();
    }
}