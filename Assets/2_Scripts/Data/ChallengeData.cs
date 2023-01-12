using UnityEngine;
using Sirenix.OdinInspector;

public enum ChallengeType
{
    NewBall = 0,
    Collect,
    Time,
    Score,
    Bounce,
    NoAim
}

[CreateAssetMenu(menuName = "Data/Challenge")]
public class ChallengeData : ScriptableObject
{
    public ChallengeType Type;

    public int NumberOfBaskets;

    [ShowIf("Type", ChallengeType.Collect)]
    public int NumberOfTokens;

    [ShowIf("Type", ChallengeType.Time)]
    public float Seconds;

    [ShowIf("Type", ChallengeType.Score)]
    public int TargetScore;

    [ShowIf("Type", ChallengeType.Bounce)]
    public int TargetBounce;
}