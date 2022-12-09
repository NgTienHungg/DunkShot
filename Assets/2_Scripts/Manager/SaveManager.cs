using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (!PlayerPrefs.HasKey("OnSound"))
            PlayerPrefs.SetInt("OnSound", 1);

        if (!PlayerPrefs.HasKey("OnVibrate"))
            PlayerPrefs.SetInt("OnVibrate", 1);

        if (!PlayerPrefs.HasKey("OnLightMode"))
            PlayerPrefs.SetInt("OnLightMode", 0);
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