using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupPassChallenge : MonoBehaviour
{
    #region ASSET
    [Header("Color")]
    [SerializeField] private Color _orange;
    [SerializeField] private Color _turquoise;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _green;
    [SerializeField] private Color _violet;
    [SerializeField] private Color _red;

    [Header("Reward")]
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
    [SerializeField] private TextMeshProUGUI _notify;
    [SerializeField] private Image _reward;
    [SerializeField] private Image _equipButton;
    [SerializeField] private Image _cancelButton;
    [SerializeField] private TextMeshProUGUI _textButton;

    private Challenge _challenge;
    private int _idSkin;

    private void OnEnable()
    {
        // enable cancel button when Type = New Ball
        if (_challenge.Type == ChallengeType.NewBall)
        {
            _cancelButton.gameObject.SetActive(true);
            _textButton.text = "EQUIP";

            _cancelButton.DOFade(0f, 0f);
            _cancelButton.DOFade(1f, 0.5f).SetDelay(0.5f);
        }
        else
        {
            _cancelButton.gameObject.SetActive(false);
            _textButton.text = "OK";
        }

        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            AudioManager.Instance.PlaySound(AudioKey.RECEIVE_TOKEN);
        });
    }

    public void Load()
    {
        _challenge = ChallengeManager.Instance.CurrentChallenge;
        _idSkin = int.Parse(_challenge.name);
        _title.text = $"CHALLENGE {_challenge.name} COMPLETED!";

        switch (_challenge.Type)
        {
            case ChallengeType.NewBall:
                _notify.text = "CHALLENGE BALL UNLOCKED!";
                _notify.color = _orange;
                _reward.sprite = DataManager.Instance.GetChallengeBall(_idSkin).Data.Sprite;
                _equipButton.sprite = _orangeButton;
                break;
            case ChallengeType.Collect:
                _notify.text = "THEME TOKENS RECEIVED!";
                _notify.color = _turquoise;
                _reward.sprite = _tokenReward;
                _equipButton.sprite = _turquoiseButton;
                break;
            case ChallengeType.Time:
                _notify.text = "THEME TOKENS RECEIVED!";
                _notify.color = _blue;
                _reward.sprite = _tokenReward;
                _equipButton.sprite = _blueButton;
                break;
            case ChallengeType.Score:
                _notify.text = "THEME TOKENS RECEIVED!";
                _notify.color = _green;
                _reward.sprite = _tokenReward;
                _equipButton.sprite = _greenButton;
                break;
            case ChallengeType.Bounce:
                _notify.text = "THEME TOKENS RECEIVED!";
                _notify.color = _violet;
                _reward.sprite = _tokenReward;
                _equipButton.sprite = _violetButton;
                break;
            case ChallengeType.NoAim:
                _notify.text = "THEME TOKENS RECEIVED!";
                _notify.color = _red;
                _reward.sprite = _tokenReward;
                _equipButton.sprite = _redButton;
                break;
        }
    }

    public void OnEquipButton()
    {
        DataManager.Instance.GetChallengeBall(_idSkin).Select();
        CanvasController.Instance.UIChallenge.CloseChallenge();
    }

    public void OnCancelButton()
    {
        CanvasController.Instance.UIChallenge.CloseChallenge();
    }
}