using DG.Tweening;
using UnityEngine;

public class BasketEffect : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private SpriteRenderer _scoreRing;
    [SerializeField] private float _normalFactor, _perfectFactor;
    private Vector3 _startScale; // (0.87, 0.62)
    private Vector3 _normalScale, _perfectScale;

    [Header("Golden")]
    [SerializeField] private Transform _beams;

    private void Awake()
    {
        _startScale = _scoreRing.transform.localScale;
        _normalScale = _normalFactor * _startScale;
        _perfectScale = _perfectFactor * _startScale;
    }

    public void Renew()
    {
        _scoreRing.transform.localScale = _startScale;
        _scoreRing.gameObject.SetActive(false);

        _beams.localScale = Vector3.zero;
        _beams.gameObject.SetActive(false);
    }

    public virtual void Score()
    {
        _scoreRing.gameObject.SetActive(true);
        _scoreRing.DOFade(1f, 0f);

        // scale
        if (ScoreManager.Instance.IsPerfect)
            _scoreRing.transform.DOScale(_perfectScale, 0.4f).SetEase(Ease.OutCubic);
        else
            _scoreRing.transform.DOScale(_normalScale, 0.4f).SetEase(Ease.OutCubic);

        // fade
        _scoreRing.DOFade(0f, 0.4f).OnComplete(() =>
        {
            _scoreRing.gameObject.SetActive(false);
        });

        if (GetComponentInParent<Basket>().IsGolden)
        {
            _beams.gameObject.SetActive(true);
            _beams.DOScale(1f, 1f).SetEase(Ease.OutExpo);
            _beams.DORotate(Vector3.forward * -360f, 10f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        }
    }

    public void Shot()
    {
    }
}