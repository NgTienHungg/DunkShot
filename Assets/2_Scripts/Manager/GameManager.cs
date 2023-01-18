using UnityEngine;

public enum GameState
{
    None,
    MainMenu,
    GamePlay,
    Paused,
    Continue,
    GameOver,
}

public enum GameMode
{
    None,
    Endless,
    Challenge,
    TrySkin
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Flash _flash;
    public Flash Flash { get => _flash; }

    public GameMode Mode { get; set; }
    public GameState State { get; set; }

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

        Application.targetFrameRate = 120;
        this.State = GameState.MainMenu;
        this.Mode = GameMode.None;
    }

    private void Start()
    {
        CanvasController.Instance.UIMainMenu.OpenApp();
    }

    private void Update()
    {
        if (this.State == GameState.MainMenu && this.Mode == GameMode.Endless && Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
        {
            Observer.OnPlayGame?.Invoke();
        }
    }
}