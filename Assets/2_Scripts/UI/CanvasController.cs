using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

    public static CanvasController Instance { get; private set; }

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

        ScoreManager.Instance.UIScore.Disable();
    }

    public void StartPlay()
    {
        _uiMainMenu.Disable();
        _uiGamePlay.Enable();
    }

    public void OnContinue()
    {
        _gameOver.SetActive(true);
        _uiContinue.Enable();
        _uiGameOver.Disable();
    }

    public void OnSecondChance()
    {
        _gameOver.SetActive(false);
        _uiContinue.Disable();
        _uiGamePlay.Enable();
    }

    public void GameOver()
    {
        _uiGamePlay.Disable();
        _gameOver.SetActive(true);
        _uiContinue.Disable();
        _uiGameOver.Enable();
    }

    public void OnPause()
    {
        _uiPaused.Enable();
    }

    public void OnResume()
    {
        _uiPaused.Disable();
    }

    public void OnBackHome()
    {
        ObjectPool.Instance.RecallAll();
        DOTween.KillAll(); // dùng nếu ReloadScene
        Time.timeScale = 1f;
        Observer.OnStartGame?.Invoke();

        GameManager.Instance.Flash.ShowTransition();

        if (GameManager.Instance.State == GameState.Paused)
        {
            _uiPaused.DisableImmediate();
        }

        _uiGamePlay.DisableImmediate();
        _uiGameOver.DisableImmediate();
        _uiContinue.DisableImmediate();
        _gameOver.SetActive(false);
        ScoreManager.Instance.UIScore.Hide();
        _uiMainMenu.Enable();

        GameManager.Instance.State = GameState.MainMenu;
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