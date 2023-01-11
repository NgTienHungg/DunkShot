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

    [Header("Customize & Challenge")]
    [SerializeField] private UICustomizeManager _uiCustomize;
    [SerializeField] private UIChallengeManager _uiChallenge;

    [Header("Flash")]
    [SerializeField] private Image flashImage;
    [SerializeField] private float targetAlpha;
    [SerializeField] private float fadeDuration;

    public static CanvasController Instance { get; private set; }
    public GameState State { get; private set; }

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
    }

    public void OnStartPlay()
    {
        _uiMainMenu.Disable();
        _uiGamePlay.Enable();
        State = GameState.GamePlay;
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
        DOTween.KillAll();

        flashImage.DOFade(targetAlpha, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            if (State == GameState.Paused)
            {
                _uiPaused.DisableImmediately();
            }

            _uiGamePlay.DisableImmediately();
            _uiGameOver.DisableImmediately();
            _uiContinue.DisableImmediately();
            _gameOver.SetActive(false);

            _uiMainMenu.Enable();

            // renew scene
            Time.timeScale = 1f;
            Controller.Instance.Renew();
            State = GameState.MainMenu;

            flashImage.DOFade(0f, fadeDuration);
        });
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
        _uiCustomize.Enable();
    }

    public void CloseCustomize()
    {
        _uiCustomize.Disable();
    }

    public void OpenChallenge()
    {
        _uiChallenge.Enable();
    }

    public void CloseChallenge()
    {
        _uiChallenge.Disable();
    }
}