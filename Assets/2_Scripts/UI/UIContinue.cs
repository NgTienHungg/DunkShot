using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIContinue : UIGame
{
    [SerializeField] private RectTransform clock;
    [SerializeField] private RectTransform ball;
    [SerializeField] private RectTransform videoAdsButton;
    [SerializeField] private RectTransform continueButton;

    [Space(10)]
    [SerializeField] private Image orangeRing;
    [SerializeField] private float waitTime;
    private float timeRemaining;

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        orangeRing.fillAmount = timeRemaining / waitTime;

        if (timeRemaining <= 0)
        {
            Controller.Instance.GameOver();
            timeRemaining = waitTime;
        }
    }

    private void OnEnable()
    {
        clock.localScale = Vector3.zero;
        videoAdsButton.localScale = Vector3.zero;
        continueButton.localScale = Vector3.zero;

        timeRemaining = waitTime;
    }

    public override void Enable()
    {
        clock.DOScale(1f, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            ball.DORotate(Vector3.forward * 20f, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });

        videoAdsButton.DOScale(1f, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            videoAdsButton.DOScale(1.1f, 0.6f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });

        continueButton.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(2f);
    }

    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnClickVideoAdButton()
    {
        //audio
        Controller.Instance.SecondChance();
    }

    public void OnClickContinueButton()
    {
        // audio
        Controller.Instance.GameOver();
    }
}