using System;
using UnityEngine;
using Sirenix.OdinInspector;

public enum ThemeType
{
    Normal,
    Season
}

[CreateAssetMenu(menuName = "Data/Theme")]
public class ThemeData : ScriptableObject
{
    public string Name;

    [PreviewField(100)]
    public Sprite ThemeCell;

    public HoopData Hoop;

    public BackgroundData Background;

    public ObstacleData Obstacle;

    public Color ScoreColor, ButtonColor, TrajectoryColor;

    public int Price;

    public ThemeType Type;
}

[Serializable]
public class HoopData
{
    public Sprite ActiveBack, ActiveFront;

    public Sprite InactiveBack, InactiveFront;
}

[Serializable]
public class BackgroundData
{
    public Sprite LightBG, DarkBG;

    public Sprite LightWall, DarkWall;

    [Range(0f, 1f)]
    public float WallSpeedFactor;

    public Sprite StaticLightWall, StaticDarkWall;

    [Range(0f, 1f)]
    public float StaticWallSpeedFactor;
}

[Serializable]
public class ObstacleData
{
    public Sprite[] Bars;

    public Sprite[] Shields;

    public Sprite Backboard;
}