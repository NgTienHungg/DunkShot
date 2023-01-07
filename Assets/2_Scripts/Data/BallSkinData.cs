using UnityEngine;
using Sirenix.OdinInspector;

public enum SkinType
{
    TradingBall,
    VideoBall,
    MissionBall,
    ChallengeBall,
    SecretBall,
    FortuneBall
}

[CreateAssetMenu(menuName = "Data/BallSkin")]
public class BallSkinData : ScriptableObject
{
    [Header("Ball")]
    [PreviewField(80)]
    public Sprite Sprite;

    [Header("Tail")]
    [PreviewField(80)]
    public Sprite TailSprite;

    public Gradient TailColor;

    [Space(20)]
    public SkinType Type;

    [ShowIf("Type", SkinType.TradingBall)]
    public int Price;

    [ShowIf("Type", SkinType.VideoBall)]
    public int NumberOfVideos;

    [TextArea]
    [ShowIf("Type", SkinType.MissionBall)]
    public string Description;
}