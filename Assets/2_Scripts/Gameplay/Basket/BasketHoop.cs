using DG.Tweening;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer _frontSprite;
    [SerializeField] private SpriteRenderer _backSprite;

    [Header("Color")]
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    [Header("Get score")]
    [SerializeField] private SpriteRenderer _scoreEff;
    [SerializeField] private Vector2 _normalScale;
    [SerializeField] private Vector2 _perfectScale;

    private void Awake()
    {
        Renew();
    }

    public void Renew()
    {
        _frontSprite.color = _activeColor;
        _backSprite.color = _activeColor;

        _scoreEff.transform.localScale = Vector3.one;
        _scoreEff.gameObject.SetActive(false);
    }

    public void Scale()
    {
        _frontSprite.color = _inactiveColor;
        _backSprite.color = _inactiveColor;

        _scoreEff.gameObject.SetActive(true);
        _scoreEff.DOFade(1f, 0f).OnComplete(() =>
        {
            // fade
            _scoreEff.DOFade(0f, 0.5f).OnComplete(() =>
            {
                _scoreEff.transform.localScale = Vector3.one;
                _scoreEff.gameObject.SetActive(false);
            });

            // scale
            if (ScoreManager.Instance.IsPerfect)
                _scoreEff.transform.DOScale(_perfectScale, 0.4f).SetEase(Ease.OutCubic);
            else
                _scoreEff.transform.DOScale(_normalScale, 0.4f).SetEase(Ease.OutCubic);
        });
    }
}