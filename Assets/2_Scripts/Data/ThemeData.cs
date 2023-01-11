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

    public ColorData Color;

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
    [Header("Background")]
    public Sprite LightBG;
    public Sprite DarkBG;

    [Header("Static")]
    public Sprite LightStaticBG;
    public Sprite DarkStaticBG;

    [Header("Wall")]
    public Sprite LightWall;
    public Sprite DarkWall;

    [Range(0f, 1f)]
    public float WallSpeed;

    [Header("Foreground")]
    public Sprite LightForeground;
    public Sprite DarkForeground;

    [Range(0f, 1f)]
    public float ForegroundSpeed;

    [Header("Decor")]
    public Sprite LightDecor;
    public Sprite DarkDecor;
}

[Serializable]
public class ObstacleData
{
    public Sprite[] Bars;

    public Sprite[] Shields;

    public Sprite Backboard;
}

[Serializable]
public class ColorData
{
    public Color LightUI, DarkUI;

    public Color LightScore, DarkScore;

    public Color LightTrajectory, DarkTrajectory;
}