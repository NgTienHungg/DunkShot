using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private void Awake()
    {
        LoadConfigGame();
    }

    private void LoadConfigGame()
    {
        if (!PlayerPrefs.HasKey(SaveKey.ON_SOUND))
            PlayerPrefs.SetInt(SaveKey.ON_SOUND, 1);

        if (!PlayerPrefs.HasKey(SaveKey.ON_VIBRATE))
            PlayerPrefs.SetInt(SaveKey.ON_VIBRATE, 1);

        if (!PlayerPrefs.HasKey(SaveKey.ON_LIGHT_MODE))
            PlayerPrefs.SetInt(SaveKey.ON_LIGHT_MODE, 0);

        if (!PlayerPrefs.HasKey(SaveKey.STAR))
            PlayerPrefs.SetInt(SaveKey.STAR, 999);

        //if (!PlayerPrefs.HasKey(SaveKey.SKIN_IN_USE))
        //{
        //    DataManager.Instance.Skins[0].Unlock();
        //    DataManager.Instance.Skins[0].Select();
        //}

        //if (!PlayerPrefs.HasKey(SaveKey.THEME_IN_USE))
        //{
        //    DataManager.Instance.Themes[0].Unlock();
        //    DataManager.Instance.Themes[0].Select();
        //}

        //if (!PlayerPrefs.HasKey(SaveKey.SKIN_IN_USE))
        //        PlayerPrefs.SetString(SaveKey.SKIN_IN_USE, "TradingBall00");

        //if (!PlayerPrefs.HasKey(SaveKey.THEME_IN_USE))
        //    PlayerPrefs.SetString(SaveKey.THEME_IN_USE, "Theme00");
    }

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}