using DG.Tweening;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [Header("Active")]
    [SerializeField] private SpriteRenderer _activeFrontSprite;
    [SerializeField] private SpriteRenderer _activeBackSprite;

    [Header("Inactive")]
    [SerializeField] private SpriteRenderer _inactiveFrontSprite;
    [SerializeField] private SpriteRenderer _inactiveBackSprite;

    [Header("Get score")]
    [SerializeField] private SpriteRenderer _scoreEff;
    [SerializeField] private Vector2 _normalScale;
    [SerializeField] private Vector2 _perfectScale;

    private void Awake()
    {
        LoadTheme();
        Renew();

        Observer.ChangeTheme += LoadTheme;
    }

    private void LoadTheme()
    {
        _activeFrontSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.ActiveFront;
        _activeBackSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.ActiveBack;

        _inactiveFrontSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.InactiveFront;
        _inactiveBackSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.InactiveBack;
    }

    public void Renew()
    {
        _activeFrontSprite.gameObject.SetActive(true);
        _activeBackSprite.gameObject.SetActive(true);

        _inactiveFrontSprite.gameObject.SetActive(false);
        _inactiveBackSprite.gameObject.SetActive(false);

        _scoreEff.transform.localScale = Vector3.one;
        _scoreEff.gameObject.SetActive(false);
    }

    public void Scale()
    {
        _activeFrontSprite.gameObject.SetActive(false);
        _activeBackSprite.gameObject.SetActive(false);

        _inactiveFrontSprite.gameObject.SetActive(true);
        _inactiveBackSprite.gameObject.SetActive(true);

        _scoreEff.gameObject.SetActive(true);
        _scoreEff.DOFade(1f, 0f).OnComplete(() =>
        {
            // fade
            _scoreEff.DOFade(0f, 0.4f).OnComplete(() =>
            {
                _scoreEff.transform.localScale = Vector3.one;
                _scoreEff.gameObject.SetActive(false);
            });

            // scale
            if (ScoreManager.Instance.IsPerfect)
                _scoreEff.transform.DOScale(_perfectScale, 0.35f).SetEase(Ease.OutCubic);
            else
                _scoreEff.transform.DOScale(_normalScale, 0.35f).SetEase(Ease.OutCubic);
        });
    }
}