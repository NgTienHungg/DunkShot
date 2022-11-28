using DG.Tweening;

public class UIMainMenu : UIGame
{
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
}