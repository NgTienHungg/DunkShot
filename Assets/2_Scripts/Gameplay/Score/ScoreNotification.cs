using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreNotification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiPerfect, uiBounce, uiScoreAdd;

    public void Renew(int comboPerfect, int comboBounce, int scoreAdded)
    {
        uiPerfect.DOFade(0f, 0f).SetUpdate(true);
        uiPerfect.rectTransform.DOLocalMoveY(80f, 0f);

        uiBounce.DOFade(0f, 0f).SetUpdate(true);
        uiBounce.rectTransform.DOLocalMoveY(50f, 0f);

        uiScoreAdd.DOFade(0f, 0f).SetUpdate(true);
        uiScoreAdd.rectTransform.DOLocalMoveY(0f, 0f);

        if (comboPerfect != 0)
            uiPerfect.text = comboPerfect == 1 ? "PERFECT!" : string.Format($"PERFECT x{comboPerfect}");

        if (comboBounce != 0)
            uiBounce.text = comboBounce == 1 ? "BOUNCE!" : string.Format($"BOUNCE x{comboBounce}");

        uiScoreAdd.text = string.Format($"+{scoreAdded}");
    }

    public void Show(int comboPerfect, int comboBounce, int scoreAdd)
    {
        Renew(comboPerfect, comboBounce, scoreAdd);

        if (comboPerfect != 0)
        {
            uiPerfect.DOFade(0.8f, 0f).OnComplete(() =>
            {
                uiPerfect.DOFade(1f, 0.2f);
                uiPerfect.rectTransform.DOLocalMoveY(180f, 1.1f).SetEase(Ease.OutQuad);
                uiPerfect.DOFade(0f, 0.3f).SetDelay(0.8f);
            });
        }

        if (comboBounce != 0)
        {
            uiBounce.DOFade(0.8f, 0f).SetDelay(0.2f).OnComplete(() =>
            {
                uiBounce.DOFade(1f, 0.2f);
                uiBounce.rectTransform.DOLocalMoveY(120f, 1f).SetEase(Ease.OutQuad);
                uiBounce.DOFade(0f, 0.3f).SetDelay(0.7f);
            });
        }

        uiScoreAdd.DOFade(0.8f, 0f).SetDelay(0.4f).OnComplete(() =>
        {
            uiScoreAdd.DOFade(1f, 0.2f);
            uiScoreAdd.rectTransform.DOLocalMoveY(50f, 0.9f).SetEase(Ease.OutQuad);
            uiScoreAdd.DOFade(0f, 0.3f).SetDelay(0.6f);
        });
    }
}