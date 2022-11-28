using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : UIGame
{
    [SerializeField] private Button videoButton;

    private void OnEnable()
    {
        canvasGroup.alpha = 0f;
        videoButton.transform.localScale = Vector3.zero;
    }

    public override void Enable()
    {
        canvasGroup.DOFade(1f, 0.3f);

        videoButton.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            videoButton.transform.DOScale(Vector3.one * 0.85f, 0.8f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });
    }

    public override void Disable()
    {
    }

    private void OnDestroy()
    {
        canvasGroup.DOKill();
        videoButton.DOKill();
    }
}