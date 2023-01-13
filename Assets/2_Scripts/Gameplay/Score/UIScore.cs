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

    public void Show()
    {
        _scoreText.gameObject.SetActive(true);
        _scoreText.DOFade(1f, 0.5f);
    }

    public void Hide()
    {
        _scoreText.DOFade(0f, 0.3f).OnComplete(() =>
        {
            _scoreText.gameObject.SetActive(false);
        });
    }

    public void Disable()
    {
        _scoreText.gameObject.SetActive(false);
    }

    public void Change()
    {
        _scoreText.text = ScoreManager.Instance.Score.ToString();

        _scoreText.transform.DOScale(1.1f, 0f).OnComplete(() =>
        {
            _scoreText.transform.DOScale(1f, 0.6f);
        });
    }
}