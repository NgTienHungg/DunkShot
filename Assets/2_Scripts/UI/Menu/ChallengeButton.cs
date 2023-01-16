using UnityEngine;
using UnityEngine.UI;

public class ChallengeButton : MonoBehaviour
{
    [SerializeField] private Image _progress;

    private int _total, _passed;

    private void Awake()
    {
        _total = ChallengeManager.Instance.Challenges.Length;
    }

    private void OnEnable()
    {
        _passed = ChallengeManager.Instance.GetCurrentLevelOfChallenge(ChallengeType.NewBall) - 1
                + ChallengeManager.Instance.GetCurrentLevelOfChallenge(ChallengeType.Collect) - 1
                + ChallengeManager.Instance.GetCurrentLevelOfChallenge(ChallengeType.Time) - 1
                + ChallengeManager.Instance.GetCurrentLevelOfChallenge(ChallengeType.Score) - 1
                + ChallengeManager.Instance.GetCurrentLevelOfChallenge(ChallengeType.Bounce) - 1
                + ChallengeManager.Instance.GetCurrentLevelOfChallenge(ChallengeType.NoAim) - 1;

        _progress.fillAmount = 1f * _passed / _total;
    }
}