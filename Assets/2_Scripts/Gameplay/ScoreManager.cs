using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI uiScore;

    [Header("Notification")]
    [SerializeField] private ScoreNotification scoreNotification;

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
        scoreNotification.Renew(0, 0, 0);
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

    private void Update()
    {
        uiScore.text = Score.ToString();
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
        // calculate score add
        if (IsPerfect) Perfect++;
        int scoreAdd = (Bounce == 0) ? (Perfect + 1) : (Perfect + 1) * 2;
        scoreAdd = Mathf.Min(scoreAdd, 20);
        Debug.Log(" => Perfect: " + IsPerfect + " --- Bounce x" + Bounce + " --- Combo x" + Perfect + " --- Add: " + scoreAdd);

        // notify
        Vector3 worldPos = Controller.Instance.BasketSpawner.GetCurrentBasket().transform.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        scoreNotification.GetComponent<RectTransform>().position = screenPos + new Vector3(0f, 65f);
        scoreNotification.Show(Perfect, Bounce, scoreAdd);

        // change score
        Score += scoreAdd;
    }

    private void ClearPerfectAndBounce()
    {
        Bounce = 0;
        IsPerfect = true;
    }
}