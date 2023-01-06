using UnityEditor;
using UnityEngine;

public class Developer
{
    [MenuItem("Developer/Clear PlayerPrefs")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}