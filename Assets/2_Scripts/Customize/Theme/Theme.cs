using UnityEngine;

public class Theme : MonoBehaviour
{
    public ThemeData Data { get; private set; }
    public int ID { get; private set; }
    public string Key { get; private set; }
    public bool Unlocked { get; private set; }

    private readonly string KEY = "Theme";
    private readonly string UNLOCKED = "UNLOCKED";

    public void SetData(ThemeData data, int index)
    {
        Data = data;
        ID = index;
        Key = KEY + index.ToString("00"); // ex: Theme01

        Unlocked = SaveSystem.GetInt(UNLOCKED + Key) == 1 ? true : false;

        if (Data.Type == ThemeType.Season && !Unlocked)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        Unlocked = true;
        SaveSystem.SetInt(UNLOCKED + Key, 1);
        Observer.OnUnlockTheme?.Invoke();
    }

    public void Select()
    {
        SaveSystem.SetString(SaveKey.THEME_IN_USE, Key);
    }
}