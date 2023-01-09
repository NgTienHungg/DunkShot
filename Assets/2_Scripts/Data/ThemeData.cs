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

    [PreviewField(50)]
    public Sprite BackHoop, FrontHoop;

    public Color HoopColor;

    public int Price;

    public ThemeType Type;
}