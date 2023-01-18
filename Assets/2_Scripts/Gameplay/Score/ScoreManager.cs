using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int BestScore { get; private set; }
    public int Score { get; private set; }
    public int ScoreAdd { get; private set; }
    public int Bounce { get; private set; }
    public int Perfect { get; private set; }
    public bool IsPerfect { get; private set; }

    [SerializeField] private UIScore _uiScore;
    public UIScore UIScore { get => _uiScore; }

    [SerializeField] private UIScoreNotify _notify;
    public UIScoreNotify Notify { get => _notify; }

    [SerializeField] private TextMeshProUGUI _bestScore;

    private void Awake()
    {
        Instance = this;
        BestScore = SaveSystem.GetInt(SaveKey.BEST_SCORE);
        RegisterListener();
    }

    private void RegisterListener()
    {
        // tắt score notify khi về menu hoặc give up challenge 
        Observer.OnStartGame += Renew;
        Observer.OnCloseChallenge += Renew;
        Observer.BallCollideHoop += DisablePerfect;
        Observer.BallCollideObstacle += CountBouncing;

        Observer.BallInBasket += ClearPerfectAndBounce;
        Observer.BallInBasketHasPoint += AddScoreAndShow;
        Observer.BallInBasketInChallenge += ClearPerfectAndBounce;
        Observer.BallInBasketHasPointInChallenge += AddScoreAndShow;
    }

    public void Renew()
    {
        Score = 0;
        Bounce = 0;
        Perfect = 0;
        IsPerfect = true;

        _uiScore.Renew();
        _notify.Renew();
    }

    private void CountBouncing()
    {
        Bounce++;
    }

    private void DisablePerfect()
    {
        IsPerfect = false;
        Perfect = 0;
    }

    private void ClearPerfectAndBounce()
    {
        Bounce = 0;
        IsPerfect = true;
    }

    private void AddScoreAndShow()
    {
        HandlePerfect();
        HandleBounce();
        CalculateScoreAdd();

        // notify
        _uiScore.Change();
        _notify.Show(Perfect, Bounce, ScoreAdd);
    }

    private void HandlePerfect()
    {
        if (IsPerfect)
        {
            Perfect++;
            Observer.Perfect?.Invoke();
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioKey.SCORE_SIMPLE);
        }

        if (Perfect == 1)
        {
            AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_1);
        }
        if (Perfect == 2)
        {
            AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_2);
            Observer.BallSmoke?.Invoke();
        }
        else if (Perfect >= 3)
        {
            if (Perfect == 3)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_3);
            else if (Perfect == 4)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_4);
            else if (Perfect == 5)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_5);
            else if (Perfect == 6)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_6);
            else if (Perfect == 7)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_7);
            else if (Perfect == 8)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_8);
            else if (Perfect == 9)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_9);
            else if (Perfect >= 10)
                AudioManager.Instance.PlaySound(AudioKey.SCORE_PERFECT_10);

            Observer.BallFlame?.Invoke();
        }
    }

    private void HandleBounce()
    {
        if (Bounce == 1)
        {
            AudioManager.Instance.PlaySound(AudioKey.SCORE_BOUNCE);
            Observer.Bounce?.Invoke();
        }
        else if (Bounce >= 2)
        {
            AudioManager.Instance.PlaySound(AudioKey.SCORE_BOUNCE);
            Observer.MultiBounce?.Invoke();
        }
    }

    private void CalculateScoreAdd()
    {
        ScoreAdd = (Bounce == 0) ? (Perfect + 1) : (Perfect + 1) * 2;
        ScoreAdd = Mathf.Min(ScoreAdd, 20);
        Observer.PointScored?.Invoke();

        // change score
        Score += ScoreAdd;
        if (Score > BestScore)
        {
            BestScore = Score;
            _bestScore.text = BestScore.ToString();
            SaveSystem.SetInt(SaveKey.BEST_SCORE, BestScore);
            Observer.NewBestScore?.Invoke();
        }
    }
}