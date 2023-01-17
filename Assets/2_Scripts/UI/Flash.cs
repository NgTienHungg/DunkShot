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
        Observer.BallFlame += ShowFlash;
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
        _flashEffect.color = _lightColor;
        _flashEffect.DOFade(0f, 0f).SetUpdate(true);
    }

    private void ApplyDarkMode()
    {
        _sceneTransition.color = _darkColor;
        _flashEffect.color = _darkColor;
        _flashEffect.DOFade(0f, 0f).SetUpdate(true);
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

    private void ShowFlash()
    {
        _flashEffect.gameObject.SetActive(true);
        _flashEffect.DOFade(0.6f, 0.15f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            _flashEffect.DOFade(0f, 0.15f).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _flashEffect.gameObject.SetActive(false);
            });
        });
    }
}