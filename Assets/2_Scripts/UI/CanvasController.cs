using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    MainMenu,
    GamePlay,
    Paused,
    Continue,
    GameOver,
    Settings,
}

public enum GameMode
{
    Endless,
    Challenge,
    TrySkin
}

public class CanvasController : MonoBehaviour
{
    [Header("UI Game States")]
    [SerializeField] private UIMainMenu _uiMainMenu;
    [SerializeField] private UIGamePlay _uiGamePlay;
    [SerializeField] private UIPaused _uiPaused;
    [SerializeField] private UISettings _uiSettings;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private UIContinue _uiContinue;
    [SerializeField] private UIGameOver _uiGameOver;

    [Header("Customize")]
    [SerializeField] private UICustomizeManager _uiCustomize;
    public UICustomizeManager UICustomize { get => _uiCustomize; }

    [Header("Challenge")]
    [SerializeField] private UIChallengeManager _uiChallenge;
    public UIChallengeManager UIChallenge { get => _uiChallenge; }

    [Header("Flash")]
    [SerializeField] private Image flashImage;
    [SerializeField] private float targetAlpha;
    [SerializeField] private float fadeDuration;

    public static CanvasController Instance { get; private set; }
    public GameState State { get; private set; }
    public GameMode Mode { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _uiMainMenu.gameObject.SetActive(true);
        _uiGamePlay.gameObject.SetActive(false);
        _uiPaused.gameObject.SetActive(false);
        _uiSettings.gameObject.SetActive(false);

        _gameOver.SetActive(false);
        _uiContinue.gameObject.SetActive(false);
        _uiGameOver.gameObject.SetActive(false);

        _uiCustomize.gameObject.SetActive(false);
        _uiChallenge.gameObject.SetActive(false);

        State = GameState.MainMenu;
        Mode = GameMode.Endless;

        ScoreManager.Instance.UIScore.Disable();
    }

    public void OnStartPlay()
    {
        _uiMainMenu.Disable();
        _uiGamePlay.Enable();

        State = GameState.GamePlay;

        if (Mode == GameMode.Endless)
        {
            ScoreManager.Instance.UIScore.Show();
        }
    }

    public void OnContinue()
    {
        _gameOver.SetActive(true);
        _uiContinue.Enable();
        _uiGameOver.Disable();
        State = GameState.Continue;
    }

    public void OnSecondChance()
    {
        _gameOver.SetActive(false);
        _uiContinue.Disable();
        _uiGamePlay.Enable();
        State = GameState.GamePlay;
    }

    public void GameOver()
    {
        _uiGamePlay.Disable();
        _gameOver.SetActive(true);
        _uiContinue.Disable();
        _uiGameOver.Enable();
        State = GameState.GameOver;
    }

    public void OnPause()
    {
        _uiPaused.Enable();
        State = GameState.Paused;
    }

    public void OnResume()
    {
        _uiPaused.Disable();
        State = GameState.GamePlay;
    }

    public void OnBackHome()
    {
        ObjectPool.Instance.RecallAll();
        DOTween.KillAll(); // dùng nếu ReloadScene
        Time.timeScale = 1f;
        Observer.OnStartGame?.Invoke();

        GameManager.Instance.Flash.ShowTransition();

        if (State == GameState.Paused)
        {
            _uiPaused.DisableImmediate();
        }

        _uiGamePlay.DisableImmediate();
        _uiGameOver.DisableImmediate();
        _uiContinue.DisableImmediate();
        _gameOver.SetActive(false);
        ScoreManager.Instance.UIScore.Hide();
        _uiMainMenu.Enable();

        // renew scene
        //Controller.Instance.StartGame();
        //Observer.OnStartGame.Invoke();
        State = GameState.MainMenu;
    }

    public void OpenSettings()
    {
        _uiSettings.Enable();
    }

    public void CloseSettings()
    {
        _uiSettings.Disable();
    }

    public void OpenCustomize()
    {
        _uiMainMenu.Disable();
        _uiCustomize.Enable();
    }

    public void CloseCustomize()
    {
        _uiMainMenu.Enable();
        _uiCustomize.Disable();
    }

    public void OpenChallenge()
    {
        _uiMainMenu.Disable();
        _uiChallenge.Enable();
    }

    public void CloseChallenge()
    {
        _uiMainMenu.Enable();
        _uiChallenge.Disable();
    }
}