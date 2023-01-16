using System;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    public static ChallengeManager Instance { get; private set; }

    [SerializeField] private Challenge[] _challenges;
    public Challenge[] Challenges { get => _challenges; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitGame();
    }

    private void InitGame()
    {
        if (SaveSystem.GetInt(SaveKey.NEW_BALL_CHALLENGE) == 0)
        {
            SaveSystem.SetInt(SaveKey.NEW_BALL_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.COLLECT_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.TIME_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.SCORE_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.BOUNCE_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.NO_AIM_CHALLENGE, 1);
        }
    }

    public void PassChallenge()
    {
        SaveSystem.SetInt(CurrentKey, CurrentLevel + 1);
    }

    public Challenge CurrentChallenge { get; private set; }

    public void SetCurrentChallenge(ChallengeType type)
    {
        CurrentChallenge = type switch
        {
            ChallengeType.NewBall => NewBallChallenge,
            ChallengeType.Collect => CollectChallenge,
            ChallengeType.Time => TimeChallenge,
            ChallengeType.Score => ScoreChallenge,
            ChallengeType.Bounce => BounceChallenge,
            ChallengeType.NoAim => NoAimChallenge,
            _ => throw new NotImplementedException(),
        };
    }

    private string CurrentKey
    {
        get
        {
            return CurrentChallenge.Type switch
            {
                ChallengeType.NewBall => SaveKey.NEW_BALL_CHALLENGE,
                ChallengeType.Collect => SaveKey.COLLECT_CHALLENGE,
                ChallengeType.Time => SaveKey.TIME_CHALLENGE,
                ChallengeType.Score => SaveKey.SCORE_CHALLENGE,
                ChallengeType.Bounce => SaveKey.BOUNCE_CHALLENGE,
                ChallengeType.NoAim => SaveKey.NO_AIM_CHALLENGE,
                _ => throw new NotImplementedException(),
            };
        }
    }

    private int CurrentLevel
    {
        get => SaveSystem.GetInt(CurrentKey);
    }

    public int GetCurrentLevelOfChallenge(ChallengeType type)
    {
        return SaveSystem.GetInt(type switch
        {
            ChallengeType.NewBall => SaveKey.NEW_BALL_CHALLENGE,
            ChallengeType.Collect => SaveKey.COLLECT_CHALLENGE,
            ChallengeType.Time => SaveKey.TIME_CHALLENGE,
            ChallengeType.Score => SaveKey.SCORE_CHALLENGE,
            ChallengeType.Bounce => SaveKey.BOUNCE_CHALLENGE,
            ChallengeType.NoAim => SaveKey.NO_AIM_CHALLENGE,
            _ => throw new NotImplementedException(),
        });
    }

    public Challenge NewBallChallenge
    {
        get => Array.Find(_challenges, challenge => challenge.Type == ChallengeType.NewBall
               && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.NEW_BALL_CHALLENGE));
    }

    public Challenge CollectChallenge
    {
        get => Array.Find(_challenges, challenge => challenge.Type == ChallengeType.Collect
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.COLLECT_CHALLENGE));
    }

    public Challenge TimeChallenge
    {
        get => Array.Find(_challenges, challenge => challenge.Type == ChallengeType.Time
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.TIME_CHALLENGE));
    }

    public Challenge ScoreChallenge
    {
        get => Array.Find(_challenges, challenge => challenge.Type == ChallengeType.Score
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.SCORE_CHALLENGE));
    }

    public Challenge BounceChallenge
    {
        get => Array.Find(_challenges, challenge => challenge.Type == ChallengeType.Bounce
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.BOUNCE_CHALLENGE));
    }

    public Challenge NoAimChallenge
    {
        get => Array.Find(_challenges, challenge => challenge.Type == ChallengeType.NoAim
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.NO_AIM_CHALLENGE));
    }
}