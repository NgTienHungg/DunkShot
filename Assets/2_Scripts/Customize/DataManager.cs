using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SkinData[] _skinDataSet;
    [SerializeField] private ThemeData[] _themeDataSet;

    private Transform _skinHolder;
    private Transform _themeHolder;

    public static DataManager Instance { get; private set; }
    public Skin[] Skins { get; private set; }
    public Theme[] Themes { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _skinHolder = transform.GetChild(0);
        _themeHolder = transform.GetChild(1);

        LoadSkinData();
        LoadThemeData();
        InitGame();
    }

    private void LoadSkinData()
    {
        Skins = new Skin[_skinDataSet.Length];

        GameObject newObj = new GameObject();
        newObj.AddComponent<Skin>();

        for (int i = 0; i < Skins.Length; i++)
        {
            Skins[i] = Instantiate(newObj, _skinHolder).GetComponent<Skin>();
            Skins[i].SetData(_skinDataSet[i]);
            Skins[i].gameObject.name = Skins[i].Key;
        }

        Destroy(newObj);
    }

    private void LoadThemeData()
    {
        Themes = new Theme[_themeDataSet.Length];

        GameObject newObj = new GameObject();
        newObj.AddComponent<Theme>();

        for (int i = 0; i < Themes.Length; i++)
        {
            Themes[i] = Instantiate(newObj, _themeHolder).GetComponent<Theme>();
            Themes[i].SetData(_themeDataSet[i], i);
            Themes[i].gameObject.name = Themes[i].Key;
        }

        Destroy(newObj);
    }

    private void InitGame()
    {
        if (!Skins[0].Unlocked)
        {
            Skins[0].Unlock();
            Skins[0].Select();
        }

        if (!Themes[0].Unlocked)
        {
            Themes[0].Unlock();
            Themes[0].Select();
        }
    }

    public Skin GetChallengeBall(int id)
    {
        return Array.Find(Skins, skin => skin.Data.Type == SkinType.Challenge && skin.ID == id);
    }

    public void UnlockChallengeBall(int id)
    {
        Array.Find(Skins, skin => skin.Data.Type == SkinType.Challenge && skin.ID == id).Unlock();
    }

    public Skin SkinInUse
    {
        get => Array.Find(Skins, skin => skin.Key == SaveSystem.GetString(SaveKey.SKIN_IN_USE));
    }

    public Theme ThemeInUse
    {
        get => Array.Find(Themes, skin => skin.Key == SaveSystem.GetString(SaveKey.THEME_IN_USE));
    }
}