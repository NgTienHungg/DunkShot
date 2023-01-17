using TMPro;
using DG.Tweening;
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
    #endregion

    [Header("Component")]
    [SerializeField] private Image _topArea;
    [SerializeField] private TextMeshProUGUI _challengeName;
    [SerializeField] private TextMeshProUGUI _basketProgress;
    private Challenge _challenge;

    [Header("New ball")]
    [SerializeField] private GameObject _ballIcon;
    [SerializeField] private Image _ball1, _ball2, _ball3;
    private int _remainingBalls;
    private int _basketPassed;

    [Header("Collect")]
    [SerializeField] private GameObject _tokenIcon;
    [SerializeField] private TextMeshProUGUI _tokenProgress;
    private int _tokenCollected;

    [Header("Time")]
    [SerializeField] private GameObject _clockIcon;
    [SerializeField] private TextMeshProUGUI _seconds, _tictac;
    private float _timeRemaining;
    private bool _isPlaying;

    [Header("Score")]
    [SerializeField] private GameObject _scoreIcon;
    [SerializeField] private TextMeshProUGUI _scoreProgress;
    private int _pointScored;

    [Header("Bounce")]
    [SerializeField] private GameObject _bounceIcon;
    [SerializeField] private TextMeshProUGUI _bounceProgress;
    private int _bouncingCount;

    private void Awake()
    {
        Observer.OnStartChallenge += ResetLevel;
        Observer.OnRestartChallenge += ResetLevel;
        Observer.OnCloseChallenge += ResetLevel;
        Observer.OnRestartChallenge += LoadChallenge;
        Observer.OnShootBall += CheckToStartCoundown;
        Observer.BallInBasketHasPointInChallenge += BallInBasketHasPoint;
        Observer.BallDeadInChallenge += BallDead;
        Observer.BallInGoldenBasket += HandlePassFail;
    }

    public void LoadChallenge()
    {
        Debug.Log("load");
        _challenge = ChallengeManager.Instance.CurrentChallenge;
        _challengeName.text = $"CHALLENGE {_challenge.name}";
        _basketProgress.text = $"{_basketPassed}/{_challenge.NumberOfBasket} HOOPS";

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
                _ball1.DOFade(1f, 0f);
                _ball2.DOFade(1f, 0f);
                _ball3.DOFade(1f, 0f);
                break;
            case ChallengeType.Collect:
                _topArea.color = _turquoise;
                _tokenIcon.SetActive(true);
                _tokenProgress.text = $"{_tokenCollected}/{_challenge.NumberOfTokens}";
                break;
            case ChallengeType.Time:
                _topArea.color = _blue;
                _clockIcon.SetActive(true);
                _timeRemaining = _challenge.TargetTime;
                _seconds.text = $"{_challenge.TargetTime} :";
                _tictac.text = $"00";
                break;
            case ChallengeType.Score:
                _topArea.color = _green;
                _scoreIcon.SetActive(true);
                _scoreProgress.text = $"{_pointScored}/{_challenge.TargetScore}";
                break;
            case ChallengeType.Bounce:
                _topArea.color = _violet;
                _bounceIcon.SetActive(true);
                _bounceProgress.text = $"{_bouncingCount}/{_challenge.TargetBounce}";
                break;
            case ChallengeType.NoAim:
                _topArea.color = _red;
                _ballIcon.SetActive(true);
                _ball1.DOFade(1f, 0f);
                _ball2.DOFade(1f, 0f);
                _ball3.DOFade(1f, 0f);
                break;
        }
    }

    private void ResetLevel()
    {
        _remainingBalls = 3;
        _basketPassed = 0;
        _tokenCollected = 0;
        _timeRemaining = _challenge.TargetTime;
        _seconds.text = _challenge.TargetTime.ToString("00 :");
        _tictac.text = "00";
        _isPlaying = false;
        _pointScored = 0;
        _bouncingCount = 0;
    }

    private void CheckToStartCoundown()
    {
        if (_challenge.Type != ChallengeType.Time || _isPlaying)
            return;

        _isPlaying = true;
    }

    private void BallInBasketHasPoint()
    {
        _basketPassed++;
        _basketProgress.text = $"{_basketPassed}/{_challenge.NumberOfBasket} HOOPS";

        _pointScored = ScoreManager.Instance.Score;
        _scoreProgress.text = $"{_pointScored}/{_challenge.TargetScore}";

        _bouncingCount += ScoreManager.Instance.Bounce;
        _bounceProgress.text = $"{_bouncingCount}/{_challenge.TargetBounce}";
    }

    private void BallDead()
    {
        _isPlaying = false;

        if (ScoreManager.Instance.Score == 0)
        {
            ResetLevel();
            Observer.FreeBallRebornInChallenge?.Invoke();
        }
        else if (_challenge.Type == ChallengeType.NewBall || _challenge.Type == ChallengeType.NoAim)
            RebornBall();
        else
            Observer.OnFailChallenge?.Invoke();
    }

    private void RebornBall()
    {
        _remainingBalls--;

        if (_remainingBalls == 2)
        {
            _ball3.DOFade(0f, 0.5f);
            Observer.BallRebornInChallenge?.Invoke();
        }
        else if (_remainingBalls == 1)
        {
            _ball2.DOFade(0f, 0.5f);
            Observer.BallRebornInChallenge?.Invoke();
        }
        else
        {
            _ball1.DOFade(0f, 0.5f);
            Observer.OnFailChallenge?.Invoke();
        }
    }

    private void HandlePassFail()
    {
        if (_challenge.Type == ChallengeType.NewBall || _challenge.Type == ChallengeType.NoAim)
        {
            if (_basketPassed == _challenge.NumberOfBasket)
                Observer.OnPassChallenge?.Invoke();
            else
                Observer.OnFailChallenge?.Invoke();
        }
        else if (_challenge.Type == ChallengeType.Collect)
        {
            Debug.LogWarning("CHUA LAM");
            Observer.OnPassChallenge?.Invoke();
        }
        else if (_challenge.Type == ChallengeType.Time)
        {
            _isPlaying = false;
            Observer.OnPassChallenge?.Invoke();
        }
        else if (_challenge.Type == ChallengeType.Score)
        {
            if (_pointScored >= _challenge.TargetScore)
                Observer.OnPassChallenge?.Invoke();
            else
                Observer.OnFailChallenge?.Invoke();
        }
        else if (_challenge.Type == ChallengeType.Bounce)
        {
            if (_bouncingCount >= _challenge.TargetBounce)
                Observer.OnPassChallenge?.Invoke();
            else
                Observer.OnFailChallenge?.Invoke();
        }
        else if (_challenge.Type == ChallengeType.Collect)
        {
            if (_basketPassed == _challenge.NumberOfBasket)
                Observer.OnPassChallenge?.Invoke();
            else
                Observer.OnFailChallenge?.Invoke();
        }
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0f;
                _seconds.text = "00 :";
                _tictac.text = "00";

                _isPlaying = false;
                Observer.OnFailChallenge?.Invoke();
                return;
            }

            _seconds.text = Mathf.Floor(_timeRemaining).ToString("00 :");
            _tictac.text = Mathf.Floor(100 * _timeRemaining % 100).ToString("00");
        }
    }

    public void OnPauseButton()
    {
        CanvasController.Instance.UIChallenge.PauseChallenge();
    }
}