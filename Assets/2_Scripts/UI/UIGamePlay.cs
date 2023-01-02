using DG.Tweening;

public class UIGamePlay : UIGame
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

    public void OnPauseButton()
    {
        // audio
        Controller.Instance.Pause();
    }
}