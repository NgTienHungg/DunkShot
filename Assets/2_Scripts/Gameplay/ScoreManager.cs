using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;

    public int Score;

    #region Singleton
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Score = 0;
    }
    #endregion

    private void OnEnable()
    {
        Observer.GetScore += AddScore;
    }

    private void OnDisable()
    {
        Observer.GetScore -= AddScore;
    }

    private void Update()
    {
        uiText.text = Score.ToString();
    }

    private void AddScore()
    {
        Score++;
    }
}