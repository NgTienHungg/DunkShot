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
public class Challenge : ScriptableObject
{
    public GameObject Level;

    public ChallengeType Type;

    public int NumberOfBasket;

    public int NumberOfTokens;

    [ShowIf("Type", ChallengeType.Time)]
    public float TargetTime;

    [ShowIf("Type", ChallengeType.Score)]
    public int TargetScore;

    [ShowIf("Type", ChallengeType.Bounce)]
    public int TargetBounce;
}