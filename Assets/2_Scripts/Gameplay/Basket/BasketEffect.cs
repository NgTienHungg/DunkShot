using DG.Tweening;
using UnityEngine;

public class BasketEffect : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] private SpriteRenderer _ring1;
    [SerializeField] private SpriteRenderer _ring2;
    [SerializeField] private SpriteRenderer _ring3;

    [Header("Score")]
    [SerializeField] private SpriteRenderer _scoreRing;
    [SerializeField] private Vector2 _normalScale, _perfectScale;

    [Header("Golden")]
    [SerializeField] private Transform _beams;

    public void Renew()
    {
        _ring1.transform.localScale = Vector3.one * 0.6f;
        _ring1.DOFade(0f, 0f).SetUpdate(true);
        _ring1.gameObject.SetActive(false);

        _ring2.transform.localScale = Vector3.one * 0.75f;
        _ring2.DOFade(0f, 0f).SetUpdate(true);
        _ring2.gameObject.SetActive(false);

        _ring3.transform.localScale = Vector3.one * 0.9f;
        _ring3.DOFade(0f, 0f).SetUpdate(true);
        _ring3.gameObject.SetActive(false);

        _scoreRing.transform.localScale = Vector3.one;
        _scoreRing.gameObject.SetActive(false);

        _beams.localScale = Vector3.zero;
        _beams.gameObject.SetActive(false);
    }

    public void Shoot()
    {
        _ring1.gameObject.SetActive(true);
        _ring1.DOFade(0f, 0f).SetDelay(0.05f).OnComplete(() =>
        {
            _ring1.DOFade(1f, 0.3f);
            _ring1.transform.DOScale(0f, 0.6f).OnComplete(() =>
            {
                _ring1.transform.localScale = Vector3.one * 0.6f;
                _ring1.DOFade(0f, 0f).SetUpdate(true);
                _ring1.gameObject.SetActive(false);
            });
        });

        _ring2.gameObject.SetActive(true);
        _ring2.DOFade(0f, 0f).SetDelay(0.1f).OnComplete(() =>
        {
            _ring2.DOFade(1f, 0.3f);
            _ring2.transform.DOScale(0f, 0.6f).OnComplete(() =>
            {
                _ring2.transform.localScale = Vector3.one * 0.75f;
                _ring2.DOFade(0f, 0f).SetUpdate(true);
                _ring2.gameObject.SetActive(false);
            });
        });

        _ring3.gameObject.SetActive(true);
        _ring3.DOFade(0f, 0f).SetDelay(0.2f).OnComplete(() =>
        {
            _ring3.DOFade(1f, 0.3f);
            _ring3.transform.DOScale(0f, 0.6f).OnComplete(() =>
            {
                _ring3.transform.localScale = Vector3.one * 0.9f;
                _ring3.DOFade(0f, 0f).SetUpdate(true);
                _ring3.gameObject.SetActive(false);
            });
        });
    }

    public void Score()
    {
        _scoreRing.gameObject.SetActive(true);
        _scoreRing.DOFade(1f, 0f);

        // scale
        if (ScoreManager.Instance.IsPerfect)
            _scoreRing.transform.DOScale(_perfectScale, 0.4f).SetEase(Ease.OutSine);
        else
            _scoreRing.transform.DOScale(_normalScale, 0.4f).SetEase(Ease.OutSine);

        // fade
        _scoreRing.DOFade(0f, 0.5f).OnComplete(() =>
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
}