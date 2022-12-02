using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Flash : MonoBehaviour
{
    public static Flash Instance { get; private set; }

    private static bool NeedFlashToOpen = false;

    [SerializeField] private float targetAlpha;
    [SerializeField] private float fadeDuration;

    private Image image;

    private void Awake()
    {
        Instance = this;

        image = GetComponent<Image>();

        if (NeedFlashToOpen)
        {
            image.color = new Color(1f, 1f, 1f, targetAlpha);
            image.raycastTarget = true;
            image.DOFade(0f, fadeDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                image.raycastTarget = false;
            });
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 0f);
            image.raycastTarget = false;
        }
    }

    public void ReloadScene()
    {
        image.raycastTarget = true;
        image.DOFade(targetAlpha, fadeDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            SceneManager.LoadScene(0);
            NeedFlashToOpen = true;
        });
    }
}