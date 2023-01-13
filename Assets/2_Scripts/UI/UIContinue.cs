using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIContinue : UIGame
{
    [SerializeField] private RectTransform _clock;
    [SerializeField] private RectTransform _ball;
    [SerializeField] private RectTransform _videoAdsButton;
    [SerializeField] private RectTransform _continueButton;

    [Space(10)]
    [SerializeField] private Image _orangeRing;
    private float _waitTime = 8f;
    private float _timeRemaining;

    private void OnEnable()
    {
        _clock.localScale = Vector3.zero;
        _videoAdsButton.localScale = Vector3.zero;
        _continueButton.localScale = Vector3.zero;

        _timeRemaining = _waitTime;
    }

    public override void Enable()
    {
        base.Enable();

        _clock.DOScale(1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _ball.DORotate(Vector3.forward * 20f, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });

        _videoAdsButton.DOScale(1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _videoAdsButton.DOScale(1.1f, 0.6f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });

        _continueButton.DOScale(1f, 0.25f).SetEase(Ease.OutBack).SetDelay(2f);
    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        _orangeRing.fillAmount = _timeRemaining / _waitTime;

        if (_timeRemaining <= 0)
        {
            CanvasController.Instance.GameOver();
            _timeRemaining = _waitTime;
        }
    }

    public void OnVideoButton()
    {
        GameController.Instance.SecondChance();
    }

    public void OnContinueButton()
    {
        CanvasController.Instance.GameOver();
    }
}