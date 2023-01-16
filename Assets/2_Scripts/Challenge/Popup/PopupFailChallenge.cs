using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupFailChallenge : MonoBehaviour
{
    #region ASSET
    [Header("Reward")]
    [SerializeField] private Sprite _ballReward;
    [SerializeField] private Sprite _tokenReward;

    [Header("Button")]
    [SerializeField] private Sprite _orangeButton;
    [SerializeField] private Sprite _turquoiseButton;
    [SerializeField] private Sprite _blueButton;
    [SerializeField] private Sprite _greenButton;
    [SerializeField] private Sprite _violetButton;
    [SerializeField] private Sprite _redButton;
    #endregion

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Image _reward;
    [SerializeField] private Image _restartButton;

    Challenge _challenge;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    public void Load()
    {
        _challenge = ChallengeManager.Instance.CurrentChallenge;
        _title.text = $"CHALLENGE {_challenge.name} FAILED!";

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

    public void OnGiveUpButton()
    {
        CanvasController.Instance.UIChallenge.CloseChallenge();
    }

    public void OnRestartButton()
    {
        CanvasController.Instance.UIChallenge.RestartChallenge();
    }
}