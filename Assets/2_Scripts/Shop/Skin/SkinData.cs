using System;
using UnityEngine;
using Sirenix.OdinInspector;

public enum SkinType
{
    Normal,
    Video,
    Mission,
    Challenge,
    Secret,
    Fortune
}

[CreateAssetMenu(menuName = "Data/Skin")]
public class SkinData : ScriptableObject
{
    [Header("Ball")]
    [PreviewField(80)]
    public Sprite Sprite;

    public TailData Tail;

    public SkinType Type;

    [ShowIf("Type", SkinType.Normal)]
    public int Price;

    [ShowIf("Type", SkinType.Video)]
    public int NumberOfVideos;

    [ShowIf("Type", SkinType.Mission)]
    public MissionData Mision;

    [ShowIf("Type", SkinType.Secret)]
    public SecretData Secret;
}

[Serializable]
public class TailData
{
    [PreviewField(80)]
    public Sprite Sprite;

    public Gradient Color;

    public Color FlashColor;
}

[Serializable]
public class MissionData
{
    public string Description;

    public int Target;
}

[Serializable]
public class SecretData
{
    public string Description;
}