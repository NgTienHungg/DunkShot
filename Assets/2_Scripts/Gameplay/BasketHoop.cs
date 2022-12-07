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
    [SerializeField] private Vector2 startScale, normalScale, perfectScale;

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
            powerRing.transform.DOScale(perfectScale, 0.4f).SetEase(Ease.OutCubic);
        else
            powerRing.transform.DOScale(normalScale, 0.4f).SetEase(Ease.OutCubic);

        powerRing.DOFade(0f, 0.5f).OnComplete(() =>
        {
            powerRing.transform.localScale = startScale;
            powerRing.DOFade(1f, 0f);
            powerRing.gameObject.SetActive(false);
        });
    }
}