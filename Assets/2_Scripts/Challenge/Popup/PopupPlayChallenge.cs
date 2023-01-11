using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlayChallenge : PopupChallenge
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
    private readonly string _timeDescription = "Complete in & seconds";
    private readonly string _scoreDescription = "Complete with score $";
    private readonly string _bounceDescription = "Compelte with $ bounces";
    private readonly string _noAimDescription = "Complete with no aim";
    #endregion

    [Header("Component")]
    [SerializeField] private Image _baner;
    [SerializeField] private Image _reward;
    [SerializeField] private Image _playButton;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    private Challenge _challenge;

    public void Show(Challenge challenge)
    {
        _challenge = challenge;
        LoadBanner();
        LoadReward();
        LoadPlayButton();
        LoadTitle();
        LoadDescription();
    }

    private void LoadBanner()
    {
        _baner.sprite = _challenge.Data.Type switch
        {
            ChallengeType.NewBall => _orangeBanner,
            ChallengeType.Collect => _turquoiseBanner,
            ChallengeType.Time => _blueBanner,
            ChallengeType.Score => _greenBanner,
            ChallengeType.Bounce => _violetBanner,
            ChallengeType.NoAim => _redBanner,
            _ => throw new System.NotImplementedException(),
        };
    }

    private void LoadReward()
    {
        _reward.sprite = _challenge.Data.Type switch
        {
            ChallengeType.NewBall => _ballReward,
            _ => _tokenReward
        };
    }

    private void LoadPlayButton()
    {
        _playButton.sprite = _challenge.Data.Type switch
        {
            ChallengeType.NewBall => _orangeButton,
            ChallengeType.Collect => _turquoiseButton,
            ChallengeType.Time => _blueButton,
            ChallengeType.Score => _greenButton,
            ChallengeType.Bounce => _violetButton,
            ChallengeType.NoAim => _redButton,
            _ => throw new System.NotImplementedException(),
        };
    }

    private void LoadTitle()
    {
        Debug.Log("LOAD TITLE");
    }

    private void LoadDescription()
    {
        _description.text = _challenge.Data.Type switch
        {
            ChallengeType.NewBall => _newBallDescription,
            ChallengeType.Collect => _collectDescription,
            ChallengeType.Time => _timeDescription,
            ChallengeType.Score => _scoreDescription,
            ChallengeType.Bounce => _bounceDescription,
            ChallengeType.NoAim => _noAimDescription,
            _ => throw new System.NotImplementedException(),
        };
    }
}