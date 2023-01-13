using DG.Tweening;

public class UIGamePlay : UIGame
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

    public override void DisableImmediate()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void OnPauseButton()
    {
        Controller.Instance.Pause();
    }
}