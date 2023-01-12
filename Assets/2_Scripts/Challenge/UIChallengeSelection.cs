using UnityEngine;
using UnityEngine.UI;

public class UIChallengeSelection : MonoBehaviour
{
    [SerializeField] private Image _progress;
    [SerializeField] private ChallengeType _type;

    public void OnClick()
    {
        ChallengeData challenge = _type switch
        {
            ChallengeType.NewBall => DataManager.Instance.NewBallChallenge,
            ChallengeType.Collect => DataManager.Instance.CollectChallenge,
            ChallengeType.Time => DataManager.Instance.TimeChallenge,
            ChallengeType.Score => DataManager.Instance.ScoreChallenge,
            ChallengeType.Bounce => DataManager.Instance.BounceChallenge,
            ChallengeType.NoAim => DataManager.Instance.NoAimChallenge,
            _ => throw new System.NotImplementedException(),
        };

        CanvasController.Instance.UIChallenge.LoadChallenge(challenge);
    }
}