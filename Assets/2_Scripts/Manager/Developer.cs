using UnityEditor;
using UnityEngine;

public class Developer
{
    [MenuItem("Developer/Clear PlayerPrefs")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Developer/Add star")]
    public static void AddStar()
    {
        SaveSystem.SetInt(SaveKey.STAR, 999);
    }
}