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

    private float normalScale = 1.5f;
    private float perfectScale = 2f;

    public void Renew()
    {
        frontHoop.color = activeColor;
        backHoop.color = activeColor;
    }

    public void OnGetScore()
    {
        frontHoop.DOColor(inactiveColor, 0.3f).SetEase(Ease.OutExpo);
        backHoop.DOColor(inactiveColor, 0.3f).SetEase(Ease.OutExpo);

        powerRing.gameObject.SetActive(true);
        powerRing.transform.DOScale(normalScale, 0.4f);
        powerRing.DOFade(0f, 0.5f).OnComplete(() =>
        {
            powerRing.transform.localScale = Vector3.one;
            powerRing.DOFade(1f, 0f);
            powerRing.gameObject.SetActive(false);
        });
    }
}