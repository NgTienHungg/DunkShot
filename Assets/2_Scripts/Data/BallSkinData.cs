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

    [TextArea]
    public string Description;

    public SkinType Type;

    [ShowIf("Type", SkinType.TradingBall)]
    public int Price;

    [Header("Tail")]
    [PreviewField(80)]
    public Sprite TailSprite;

    public Gradient TailColor;
}