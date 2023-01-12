using DG.Tweening;
using UnityEngine;

public class BasketEffect : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private SpriteRenderer _scoreRing;
    [SerializeField] private float _normalFactor, _perfectFactor;
    private Vector3 _startScale; // (0.87, 0.62)
    private Vector3 _normalScale, _perfectScale;

    private void Awake()
    {
        _startScale = _scoreRing.transform.localScale;
        _normalScale = _normalFactor * _startScale;
        _perfectScale = _perfectFactor * _startScale;

        Renew();
    }

    public void Renew()
    {
        _scoreRing.transform.localScale = _startScale;
        _scoreRing.gameObject.SetActive(false);
    }

    public void Score()
    {
        _scoreRing.gameObject.SetActive(true);
        _scoreRing.DOFade(1f, 0f);

        // scale
        if (ScoreManager.Instance.IsPerfect)
        {
            _scoreRing.transform.DOScale(_perfectScale, 0.4f).SetEase(Ease.OutCubic);
        }
        else
        {
            _scoreRing.transform.DOScale(_normalScale, 0.4f).SetEase(Ease.OutCubic);
        }

        // fade
        _scoreRing.DOFade(0f, 0.4f).OnComplete(() =>
        {
            _scoreRing.gameObject.SetActive(false);
        });
    }

    public void Shot()
    {
    }
}