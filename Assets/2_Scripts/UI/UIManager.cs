using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Game States")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIGamePlay uiGamePlay;
    [SerializeField] private UIPaused uiPaused;
    [SerializeField] private UISettings uiSettings;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverControl;
    [SerializeField] private UIContinue uiContinue;
    [SerializeField] private UIGameOver uiGameOver;

    [Header("Customize & Challenge")]
    [SerializeField] private CustomizeManager _uiCustomize;
    [SerializeField] private ChallengeManager _uiChallenge;

    [Header("Flash")]
    [SerializeField] private Image flashImage;
    [SerializeField] private float targetAlpha;
    [SerializeField] private float fadeDuration;

    private static UIManager _instance;
    public static UIManager Instance { get => _instance; }

    private GameState _state;
    public GameState State { get => _state; }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        uiMainMenu.gameObject.SetActive(true);
        uiGamePlay.gameObject.SetActive(false);
        uiPaused.gameObject.SetActive(false);
        uiSettings.gameObject.SetActive(false);

        gameOverControl.SetActive(false);
        uiContinue.gameObject.SetActive(false);
        uiGameOver.gameObject.SetActive(false);

        _uiCustomize.gameObject.SetActive(false);
        _uiChallenge.gameObject.SetActive(false);

        _state = GameState.MainMenu;
    }

    public void OnStartPlay()
    {
        uiMainMenu.Disable();

        uiGamePlay.gameObject.SetActive(true);
        uiGamePlay.Enable();

        _state = GameState.GamePlay;
    }

    public void OnContinue()
    {
        gameOverControl.SetActive(true);

        uiContinue.gameObject.SetActive(true);
        uiContinue.Enable();

        uiGameOver.Disable();

        _state = GameState.Continue;
    }

    public void OnSecondChance()
    {
        gameOverControl.SetActive(false);

        uiContinue.Disable();

        uiGamePlay.gameObject.SetActive(true);
        uiGamePlay.Enable();

        _state = GameState.GamePlay;
    }

    public void OnGameOver()
    {
        uiGamePlay.Disable();

        gameOverControl.SetActive(true);

        uiContinue.Disable();

        uiGameOver.gameObject.SetActive(true);
        uiGameOver.Enable();

        _state = GameState.GameOver;
    }

    public void OnPause()
    {
        uiPaused.gameObject.SetActive(true);
        uiPaused.Enable();

        _state = GameState.Paused;
    }

    public void OnResume()
    {
        uiPaused.Disable();

        _state = GameState.GamePlay;
    }

    public void OnSettings()
    {
        uiSettings.gameObject.SetActive(true);
        uiSettings.Enable();
    }

    public void OnCloseSettings()
    {
        uiSettings.Disable();
    }

    public void OnBackHome()
    {
        ObjectPool.Instance.RecallAll();
        DOTween.KillAll();

        flashImage.DOFade(targetAlpha, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            uiPaused.DisableImmediately();
            uiGamePlay.DisableImmediately();
            gameOverControl.SetActive(false);

            uiMainMenu.Enable();

            // renew scene
            Time.timeScale = 1f;
            Controller.Instance.Renew();
            _state = GameState.MainMenu;

            flashImage.DOFade(0f, fadeDuration).SetUpdate(true);
        });

        //uiPaused.DisableImmediately();
        //uiGamePlay.DisableImmediately();
        //gameOverControl.SetActive(false);

        //uiMainMenu.Enable();

        //// renew scene
        //Time.timeScale = 1f;
        //Controller.Instance.Renew();
        //_state = GameState.MainMenu;
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