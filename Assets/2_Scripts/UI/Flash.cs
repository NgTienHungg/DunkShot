using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [Header("Transition")]
    [SerializeField] private Image _sceneTransition;
    [SerializeField] private Color _lightColor, _darkColor;
    [SerializeField] private float _fadeDuration = 0.15f;

    [Header("Transition")]
    [SerializeField] private Image _flashEffect;

    private void Awake()
    {
        SetupImage();
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnLightMode += ApplyLightMode;
        Observer.OnDarkMode += ApplyDarkMode;
    }

    private void SetupImage()
    {
        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
            ApplyLightMode();
        else
            ApplyDarkMode();

        _sceneTransition.gameObject.SetActive(false);
        _flashEffect.gameObject.SetActive(false);
    }

    private void ApplyLightMode()
    {
        _sceneTransition.color = _lightColor;
    }

    private void ApplyDarkMode()
    {
        _sceneTransition.color = _darkColor;
    }

    public void ShowTransition()
    {
        _sceneTransition.gameObject.SetActive(true);
        _sceneTransition.DOFade(0f, _fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            _sceneTransition.DOFade(1f, 0f).SetUpdate(true);
            _sceneTransition.gameObject.SetActive(false);
        });
    }
}