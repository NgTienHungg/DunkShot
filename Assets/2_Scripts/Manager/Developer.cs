using UnityEngine;
using UnityEditor;

public class Developer
{
    [MenuItem("Developer/Clear PlayerPrefs")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}