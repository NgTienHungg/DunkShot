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
    [PreviewField(80), HideLabel]
    [HorizontalGroup("Split", 80)]
    public Sprite Sprite;

    [TextArea]
    [VerticalGroup("Split/Right")]
    public string Description;

    [VerticalGroup("Split/Right"), LabelWidth(50)]
    public SkinType Type;

    [ShowIf("Type", SkinType.TradingBall)]
    public int Price;
}