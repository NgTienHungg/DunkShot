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

    [Header("Challenge")]
    [SerializeField] private ChallengeData[] _challengeDataSet;
    public ChallengeData[] ChallengeDataSet { get => _challengeDataSet; }
    public ChallengeData CurrentChallenge { get; set; }

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

        if (SaveSystem.GetInt(SaveKey.NEW_BALL_CHALLENGE) == 0)
        {
            SaveSystem.SetInt(SaveKey.NEW_BALL_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.COLLECT_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.TIME_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.SCORE_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.BOUNCE_CHALLENGE, 1);
            SaveSystem.SetInt(SaveKey.NO_AIM_CHALLENGE, 1);
        }
    }

    public Skin SkinInUse
    {
        get => Array.Find(Skins, skin => skin.Key == SaveSystem.GetString(SaveKey.SKIN_IN_USE));
    }

    public Theme ThemeInUse
    {
        get => Array.Find(Themes, skin => skin.Key == SaveSystem.GetString(SaveKey.THEME_IN_USE));
    }

    public ChallengeData NewBallChallenge
    {
        get => Array.Find(_challengeDataSet, challenge => challenge.Type == ChallengeType.NewBall
               && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.NEW_BALL_CHALLENGE));
    }

    public ChallengeData CollectChallenge
    {
        get => Array.Find(_challengeDataSet, challenge => challenge.Type == ChallengeType.Collect
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.COLLECT_CHALLENGE));
    }

    public ChallengeData TimeChallenge
    {
        get => Array.Find(_challengeDataSet, challenge => challenge.Type == ChallengeType.Time
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.TIME_CHALLENGE));
    }

    public ChallengeData ScoreChallenge
    {
        get => Array.Find(_challengeDataSet, challenge => challenge.Type == ChallengeType.Score
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.SCORE_CHALLENGE));
    }

    public ChallengeData BounceChallenge
    {
        get => Array.Find(_challengeDataSet, challenge => challenge.Type == ChallengeType.Bounce
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.BOUNCE_CHALLENGE));
    }

    public ChallengeData NoAimChallenge
    {
        get => Array.Find(_challengeDataSet, challenge => challenge.Type == ChallengeType.NoAim
            && int.Parse(challenge.name) == SaveSystem.GetInt(SaveKey.NO_AIM_CHALLENGE));
    }
}