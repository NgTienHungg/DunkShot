using UnityEngine;
using Sirenix.OdinInspector;

public enum ChallengeType
{
    NewBall,
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

    [ShowIf("Type", ChallengeType.NewBall)]
    public int NumberOfBaskets;
}