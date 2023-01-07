using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Holder")]
    [SerializeField] private Transform _ballSkinHolder;
    [SerializeField] private Transform _themeHolder;

    [Header("Data")]
    [SerializeField] private BallSkinData[] _ballSkinDataSet;
    [SerializeField] private ThemeData[] _themeDataSet;

    public static DataManager Instance { get; private set; }
    public BallSkin[] BallSkins { get; private set; }
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

        LoadBallSkinData();
        LoadThemeData();
        InitGame();
    }

    private void LoadBallSkinData()
    {
        BallSkins = new BallSkin[_ballSkinDataSet.Length];

        // create a temporary gameobject
        GameObject newObj = new GameObject();
        newObj.AddComponent<BallSkin>();

        // instantiate all skin in holder, and set data
        for (int i = 0; i < BallSkins.Length; i++)
        {
            BallSkins[i] = Instantiate(newObj, _ballSkinHolder).GetComponent<BallSkin>();
            BallSkins[i].SetData(_ballSkinDataSet[i], i);
            BallSkins[i].gameObject.name = BallSkins[i].Name;
        }

        Destroy(newObj);
    }

    private void LoadThemeData()
    {
    }

    private void InitGame()
    {
        if (!BallSkins[0].Unlocked)
        {
            BallSkins[0].Unlock();
            SaveSystem.SetString(SaveKey.BALL_SKIN_IN_USE, BallSkins[0].Name);
        }
    }

    public BallSkin BallSkinInUse
    {
        get
        {
            string currentSkinName = SaveSystem.GetString(SaveKey.BALL_SKIN_IN_USE);
            foreach (var ballSkin in BallSkins)
            {
                if (ballSkin.Name == currentSkinName)
                {
                    return ballSkin;
                }
            }
            return null;
        }
    }
}