using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static SaveSystem _instance;
    public static SaveSystem Instance { get => _instance; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
    }

    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
}