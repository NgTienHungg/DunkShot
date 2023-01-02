using TMPro;
using DG.Tweening;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Renew()
    {
        _scoreText.text = "0";
    }

    public void Change()
    {
        _scoreText.text = ScoreManager.Instance.Score.ToString();

        _scoreText.transform.DOScale(1.2f, 0f).OnComplete(() =>
        {
            _scoreText.transform.DOScale(1f, 0.8f).SetEase(Ease.OutCubic);
        });
    }
}