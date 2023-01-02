using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Holder")]
    [SerializeField] private Transform _ballSkinHolder;
    [SerializeField] private Transform _themeHolder;

    [Header("Data")]
    [SerializeField] private BallSkinData[] _ballSkinDataSet;
    [SerializeField] private ThemeData[] _themeDataSet;

    private static DataManager _instance;
    public static DataManager Instance { get => _instance; }

    private BallSkin[] _ballSkins;
    public BallSkin[] BallSkins { get => _ballSkins; }

    private Theme[] _themes;
    public Theme[] Themes { get => _themes; }

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

        LoadBallSkinData();
        LoadThemeData();
    }

    private void LoadBallSkinData()
    {
        _ballSkins = new BallSkin[_ballSkinDataSet.Length];

        // create a temporary gameobject
        GameObject newObj = new GameObject();
        newObj.AddComponent<BallSkin>();

        // instantiate all skin in holder, and set data
        for (int i = 0; i < _ballSkins.Length; i++)
        {
            _ballSkins[i] = Instantiate(newObj, _ballSkinHolder).GetComponent<BallSkin>();
            _ballSkins[i].SetData(_ballSkinDataSet[i], i);
            _ballSkins[i].gameObject.name = _ballSkins[i].Name;
        }

        _ballSkins[0].Unlock();
    }

    private void LoadThemeData()
    {
    }
}