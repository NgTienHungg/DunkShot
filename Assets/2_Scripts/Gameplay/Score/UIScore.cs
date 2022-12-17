using TMPro;
using DG.Tweening;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    public void Awake()
    {
        scoreText= GetComponent<TextMeshProUGUI>();
    }

    public void Renew()
    {
        scoreText.text = "0";
    }

    public void Change()
    {
        scoreText.text = ScoreManager.Instance.Score.ToString();

        scoreText.transform.DOScale(1.2f, 0f).OnComplete(() =>
        {
            scoreText.transform.DOScale(1f, 0.8f).SetEase(Ease.OutCubic);
        });
    }
}