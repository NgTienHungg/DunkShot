using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    private int score = 0;

    private void OnEnable()
    {
        GameEvent.GetScore += AddScore;
    }

    private void OnDisable()
    {
        GameEvent.GetScore -= AddScore;
    }

    private void Update()
    {
        uiText.text = score.ToString();
    }

    private void AddScore()
    {
        score++;
    }
}