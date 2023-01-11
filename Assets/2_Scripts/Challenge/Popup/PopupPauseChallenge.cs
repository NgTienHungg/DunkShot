using UnityEngine;
using UnityEngine.UI;

public class PopupPauseChallenge : MonoBehaviour
{
    #region ASSET
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
    #endregion

    [Header("Component")]
    [SerializeField] private Image _reward;
    [SerializeField] private Image _restartButton;

    private Challenge _challenge;

    public void Show(Challenge challenge)
    {
        _challenge = challenge;
        LoadReward();
        LoadRestartButton();
    }

    private void LoadReward()
    {
        _reward.sprite = _challenge.Data.Type switch
        {
            ChallengeType.NewBall => _ballReward,
            _ => _tokenReward
        };
    }

    private void LoadRestartButton()
    {
        _restartButton.sprite = _challenge.Data.Type switch
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
}