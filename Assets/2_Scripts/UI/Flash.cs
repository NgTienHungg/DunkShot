using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [Header("Transition")]
    [SerializeField] private Image _sceneTransition;
    [SerializeField] private Color _lightColor, _darkColor;
    [SerializeField] private float _fadeDuration = 0.3f;

    [Header("Transition")]
    [SerializeField] private Image _flashEffect;
    [SerializeField] private float _flashStartAlpha = 0.5f;
    [SerializeField] private float _flashDuration = 0.2f;
    private void Awake()
    {
        SetupImage();
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnChangeSkin += ChangeSkin;
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

        _flashEffect.color = DataManager.Instance.SkinInUse.Data.Tail.FlashColor;
        _flashEffect.gameObject.SetActive(false);
    }

    private void ChangeSkin()
    {
        _flashEffect.color = DataManager.Instance.SkinInUse.Data.Tail.FlashColor;
        _flashEffect.DOFade(_flashStartAlpha, 0f).SetUpdate(true);
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

    private void ShowFlash()
    {
        _flashEffect.gameObject.SetActive(true);
        _flashEffect.DOFade(0f, _flashDuration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _flashEffect.DOFade(_flashStartAlpha, 0f);
            _flashEffect.gameObject.SetActive(false);
        });
    }
}