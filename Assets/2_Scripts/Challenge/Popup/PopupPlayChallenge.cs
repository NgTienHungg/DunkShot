using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlayChallenge : MonoBehaviour
{
    #region ASSET
    [Header("Banner")]
    [SerializeField] private Sprite _orangeBanner;
    [SerializeField] private Sprite _turquoiseBanner;
    [SerializeField] private Sprite _blueBanner;
    [SerializeField] private Sprite _greenBanner;
    [SerializeField] private Sprite _violetBanner;
    [SerializeField] private Sprite _redBanner;

    [Header("Button")]
    [SerializeField] private Sprite _orangeButton;
    [SerializeField] private Sprite _turquoiseButton;
    [SerializeField] private Sprite _blueButton;
    [SerializeField] private Sprite _greenButton;
    [SerializeField] private Sprite _violetButton;
    [SerializeField] private Sprite _redButton;

    [Header("Reward")]
    [SerializeField] private Sprite _ballReward;
    [SerializeField] private Sprite _tokenReward;

    private readonly string _newBallDescription = "Score $ hoops";
    private readonly string _collectDescription = "Collect all tokens";
    private readonly string _timeDescription = "Complete in $ seconds";
    private readonly string _scoreDescription = "Complete with score $";
    private readonly string _bounceDescription = "Compelte with $ bounces";
    private readonly string _noAimDescription = "Complete with no aim";
    #endregion

    [Header("Component")]
    [SerializeField] private Image _baner;
    [SerializeField] private Image _reward;
    [SerializeField] private Image _playButton;
    [SerializeField] private Image _cancelButton;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    private Challenge _challenge;

    private void OnEnable()
    {
        _cancelButton.gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(0.1f).OnComplete(() =>
        {
            _cancelButton.gameObject.SetActive(true);
            _cancelButton.color = new Color(1, 1, 1, 0);
            _cancelButton.DOFade(1f, 0.5f);

            _playButton.transform.DOScale(0.96f, 0.8f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });
    }

    public void Load()
    {
        _challenge = ChallengeManager.Instance.CurrentChallenge;
        _title.text = $"CHALLENGE {_challenge.name}";

        switch (_challenge.Type)
        {
            case ChallengeType.NewBall:
                _baner.sprite = _orangeBanner;
                _reward.sprite = _ballReward;
                _playButton.sprite = _orangeButton;
                _description.text = _newBallDescription.Replace("$", _challenge.NumberOfBaskets.ToString());
                break;
            case ChallengeType.Collect:
                _baner.sprite = _turquoiseBanner;
                _reward.sprite = _tokenReward;
                _playButton.sprite = _turquoiseButton;
                _description.text = _collectDescription.Replace("$", _challenge.NumberOfTokens.ToString());
                break;
            case ChallengeType.Time:
                _baner.sprite = _blueBanner;
                _reward.sprite = _tokenReward;
                _playButton.sprite = _blueButton;
                _description.text = _timeDescription.Replace("$", _challenge.Seconds.ToString());
                break;
            case ChallengeType.Score:
                _baner.sprite = _greenBanner;
                _reward.sprite = _tokenReward;
                _playButton.sprite = _greenButton;
                _description.text = _scoreDescription.Replace("$", _challenge.TargetScore.ToString());
                break;
            case ChallengeType.Bounce:
                _baner.sprite = _violetBanner;
                _reward.sprite = _tokenReward;
                _playButton.sprite = _violetButton;
                _description.text = _bounceDescription.Replace("$", _challenge.TargetBounce.ToString());
                break;
            case ChallengeType.NoAim:
                _baner.sprite = _redBanner;
                _reward.sprite = _tokenReward;
                _playButton.sprite = _redButton;
                _description.text = _noAimDescription.Replace("$", _challenge.NumberOfBaskets.ToString());
                break;
        }
    }

    public void OnPlayButton()
    {
        CanvasController.Instance.UIChallenge.PlayChallenge();
    }

    public void OnCancelButton()
    {
        CanvasController.Instance.UIChallenge.CloseChallenge();
    }
}