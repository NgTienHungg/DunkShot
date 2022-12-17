using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIScore uiScore;

    [SerializeField] private UIScoreNotify notify;

    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }
    public int Bounce { get; private set; }
    public int Perfect { get; private set; }
    public bool IsPerfect { get; private set; }


    private void Awake()
    {
        Instance = this;
        Renew();
    }

    public void Renew()
    {
        Score = 0;
        Bounce = 0;
        Perfect = 0;
        IsPerfect = true;

        uiScore.Renew();
        notify.Renew();
    }

    private void OnEnable()
    {
        Observer.BallCollideObstacle += CountBouncing;
        Observer.BallCollideHoop += DisablePerfect;
        Observer.GetScore += AddScore;
        Observer.BallInBasket += ClearPerfectAndBounce;
    }

    private void OnDisable()
    {
        Observer.BallCollideObstacle -= CountBouncing;
        Observer.BallCollideHoop -= DisablePerfect;
        Observer.GetScore -= AddScore;
        Observer.BallInBasket -= ClearPerfectAndBounce;
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

    private void AddScore()
    {
        // handle perfect
        if (IsPerfect)
            Perfect++;

        if (Perfect == 2)
            Observer.BallSmoke?.Invoke();
        else if (Perfect == 3)
            Observer.BallFlame?.Invoke();

        // calculate score add
        int scoreAdd = (Bounce == 0) ? (Perfect + 1) : (Perfect + 1) * 2;
        scoreAdd = Mathf.Min(scoreAdd, 20);
        Debug.Log($" => Perfect x{IsPerfect} --- Bounce x{Bounce} --- Combo x{Perfect} --- Add: {scoreAdd}");

        // change score
        Score += scoreAdd;

        // notify
        uiScore.Change();
        notify.Show(Perfect, Bounce, scoreAdd);
    }

    private void ClearPerfectAndBounce()
    {
        Bounce = 0;
        IsPerfect = true;
    }
}