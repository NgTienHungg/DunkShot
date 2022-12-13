using DG.Tweening;
using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private SpriteRenderer activeFrontSprite;
    [SerializeField] private SpriteRenderer activeBackSprite;

    [Header("Color")]
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    [Header("Power ring")]
    [SerializeField] private SpriteRenderer powerRing;
    [SerializeField] private Vector2 startScale;
    [SerializeField] private Vector2 normalScale;
    [SerializeField] private Vector2 perfectScale;

    public void Renew()
    {
        activeFrontSprite.color = activeColor;
        activeBackSprite.color = activeColor;

        powerRing.gameObject.SetActive(false);
        powerRing.transform.localScale = startScale;
    }

    public void Scale()
    {
        activeFrontSprite.color = inactiveColor;
        activeBackSprite.color = inactiveColor;

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