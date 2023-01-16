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

    private ChallengeData _challenge;

    public void Load()
    {
        _challenge = DataManager.Instance.CurrentChallenge;

        switch (_challenge.Type)
        {
            case ChallengeType.NewBall:
                _reward.sprite = _ballReward;
                _restartButton.sprite = _orangeButton;
                break;
            case ChallengeType.Collect:
                _reward.sprite = _tokenReward;
                _restartButton.sprite = _turquoiseButton;
                break;
            case ChallengeType.Time:
                _reward.sprite = _tokenReward;
                _restartButton.sprite = _blueButton;
                break;
            case ChallengeType.Score:
                _reward.sprite = _tokenReward;
                _restartButton.sprite = _greenButton;
                break;
            case ChallengeType.Bounce:
                _reward.sprite = _tokenReward;
                _restartButton.sprite = _violetButton;
                break;
            case ChallengeType.NoAim:
                _reward.sprite = _tokenReward;
                _restartButton.sprite = _redButton;
                break;
        }
    }

    public void OnRestartButton()
    {
        Debug.Log("RESTART");
    }

    public void OnCancelButton()
    {
        CanvasController.Instance.UIChallenge.Resume();
    }

    public void OnGiveUpButton()
    {
        CanvasController.Instance.UIChallenge.CloseChallenge();
    }
}