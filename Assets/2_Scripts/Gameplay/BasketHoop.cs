using DG.Tweening;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [Header("Hoop")]
    [SerializeField] private SpriteRenderer frontHoop;
    [SerializeField] private SpriteRenderer backHoop;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    [Header("Power ring")]
    [SerializeField] private SpriteRenderer powerRing;
    [SerializeField] private float normalFactor, perfectFactor;
    private Vector2 startScale;

    private void Start()
    {
        startScale = transform.localScale;
        Renew();
    }

    public void Renew()
    {
        frontHoop.color = activeColor;
        backHoop.color = activeColor;

        powerRing.gameObject.SetActive(false);
        powerRing.transform.localScale = startScale;
    }

    public void OnGetScore()
    {
        frontHoop.color = inactiveColor;
        backHoop.color = inactiveColor;

        powerRing.gameObject.SetActive(true);

        if (ScoreManager.Instance.IsPerfect)
            powerRing.transform.DOScale(startScale * perfectFactor, 0.25f).SetEase(Ease.OutCirc);
        else
            powerRing.transform.DOScale(startScale * normalFactor, 0.25f).SetEase(Ease.OutCirc);

        powerRing.DOFade(0f, 0.5f).OnComplete(() =>
        {
            powerRing.transform.localScale = startScale;
            powerRing.DOFade(1f, 0f);
            powerRing.gameObject.SetActive(false);
        });
    }
}