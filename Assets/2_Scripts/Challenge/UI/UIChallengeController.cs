using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeController : MonoBehaviour
{
    #region ASSET
    [Header("Color")]
    [SerializeField] private Color _orange;
    [SerializeField] private Color _turquoise;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _green;
    [SerializeField] private Color _violet;
    [SerializeField] private Color _red;

    [Header("Icon")]
    [SerializeField] private GameObject _ballIcon;
    [SerializeField] private GameObject _tokenIcon;
    [SerializeField] private GameObject _clockIcon;
    [SerializeField] private GameObject _scoreIcon;
    [SerializeField] private GameObject _bounceIcon;
    #endregion

    [Header("Component")]
    [SerializeField] private Image _topArea;
    [SerializeField] private TextMeshProUGUI _challengeName;
    [SerializeField] private TextMeshProUGUI _basketAmount;

    private Challenge _challenge;

    private int _basketPassed;

    public void LoadChallenge()
    {
        _challenge = ChallengeManager.Instance.CurrentChallenge;
        _challengeName.text = $"CHALLENGE {_challenge.name}";
        _basketAmount.text = $"{_basketPassed}/{_challenge.NumberOfBaskets} HOOPS";

        _ballIcon.SetActive(false);
        _tokenIcon.SetActive(false);
        _clockIcon.SetActive(false);
        _scoreIcon.SetActive(false);
        _bounceIcon.SetActive(false);

        switch (_challenge.Type)
        {
            case ChallengeType.NewBall:
                _topArea.color = _orange;
                _ballIcon.SetActive(true);
                break;
            case ChallengeType.Collect:
                _topArea.color = _turquoise;
                _tokenIcon.SetActive(true);
                break;
            case ChallengeType.Time:
                _topArea.color = _blue;
                _clockIcon.SetActive(true);
                break;
            case ChallengeType.Score:
                _topArea.color = _green;
                _scoreIcon.SetActive(true);
                break;
            case ChallengeType.Bounce:
                _topArea.color = _violet;
                _bounceIcon.SetActive(true);
                break;
            case ChallengeType.NoAim:
                _topArea.color = _red;
                _ballIcon.SetActive(true);
                break;
        }
    }

    public void OnPauseButton()
    {
        CanvasController.Instance.UIChallenge.PauseChallenge();
    }
}