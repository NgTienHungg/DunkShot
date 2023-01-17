using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }
    public int Bounce { get; private set; }
    public int Perfect { get; private set; }
    public bool IsPerfect { get; private set; }

    [SerializeField] private UIScore _uiScore;
    public UIScore UIScore { get => _uiScore; }

    [SerializeField] private UIScoreNotify _notify;
    public UIScoreNotify Notify { get => _notify; }

    private void Awake()
    {
        Instance = this;
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
            Perfect++;

        if (Perfect == 2)
            Observer.BallSmoke?.Invoke();
        else if (Perfect == 3)
            Observer.BallFlame?.Invoke();

        // calculate score add
        int scoreAdd = (Bounce == 0) ? (Perfect + 1) : (Perfect + 1) * 2;
        scoreAdd = Mathf.Min(scoreAdd, 20);
        Debug.Log($" => IsPerfect = {IsPerfect} --- Bounce x{Bounce} --- Perfect x{Perfect} --- ScoreAdd: {scoreAdd}");

        // change score
        Score += scoreAdd;

        // notify
        _uiScore.Change();
        _notify.Show(Perfect, Bounce, scoreAdd);
    }

    private void ClearPerfectAndBounce()
    {
        Bounce = 0;
        IsPerfect = true;
    }
}