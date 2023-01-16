using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChallengeSelection : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private ChallengeType _type;

    private int _total;

    private void Awake()
    {
        foreach (ChallengeData challenge in DataManager.Instance.ChallengeDataSet)
        {
            if (challenge.Type == _type)
            {
                _total += 1;
            }
        }
    }

    private void OnEnable()
    {
        int now = -1 + SaveSystem.GetInt(_type switch
        {
            ChallengeType.NewBall => SaveKey.NEW_BALL_CHALLENGE,
            ChallengeType.Collect => SaveKey.COLLECT_CHALLENGE,
            ChallengeType.Time => SaveKey.TIME_CHALLENGE,
            ChallengeType.Score => SaveKey.SCORE_CHALLENGE,
            ChallengeType.Bounce => SaveKey.BOUNCE_CHALLENGE,
            ChallengeType.NoAim => SaveKey.NO_AIM_CHALLENGE,
            _ => throw new System.NotImplementedException(),
        });

        _fill.fillAmount = 1f * now / _total;
        _progress.text = (100 * now / _total) + "%";
    }

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

        DataManager.Instance.CurrentChallenge = challenge;
        CanvasController.Instance.UIChallenge.LoadChallenge();
    }
}