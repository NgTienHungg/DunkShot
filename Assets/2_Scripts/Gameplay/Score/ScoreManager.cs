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

    private void AddScoreAndShow()
    {
        if (IsPerfect)
        {
            Perfect++;
            Observer.Perfect?.Invoke();
        }

        if (Bounce == 1)
            Observer.Bounce?.Invoke();
        else if (Bounce >= 2)
            Observer.MultiBounce?.Invoke();

        if (Perfect == 2)
            Observer.BallSmoke?.Invoke();
        else if (Perfect == 3)
            Observer.BallFlame?.Invoke();

        // calculate score add
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

        // notify
        _uiScore.Change();
        _notify.Show(Perfect, Bounce, ScoreAdd);
    }

    private void ClearPerfectAndBounce()
    {
        Bounce = 0;
        IsPerfect = true;
    }
}