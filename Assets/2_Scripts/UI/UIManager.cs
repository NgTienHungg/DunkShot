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

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIGamePlay uiGamePlay;
    [SerializeField] private UIPaused uiPaused;
    [SerializeField] private UISettings uiSettings;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverControl;
    [SerializeField] private UIContinue uiContinue;
    [SerializeField] private UIGameOver uiGameOver;

    [Header("Flash")]
    [SerializeField] private Image flashImage;
    [SerializeField] private float targetAlpha;
    [SerializeField] private float fadeDuration;

    public static UIManager Instance { get; private set; }

    [HideInInspector] public GameState state;

    private void Awake()
    {
        Instance = this;

        uiMainMenu.gameObject.SetActive(true);
        uiGamePlay.gameObject.SetActive(false);
        uiPaused.gameObject.SetActive(false);
        uiSettings.gameObject.SetActive(false);

        gameOverControl.SetActive(false);
        uiContinue.gameObject.SetActive(false);
        uiGameOver.gameObject.SetActive(false);

        flashImage.color = new Color(1f, 1f, 1f, 0f);

        state = GameState.MainMenu;
    }

    public void OnStartPlay()
    {
        uiMainMenu.Disable();

        uiGamePlay.gameObject.SetActive(true);
        uiGamePlay.Enable();

        state = GameState.GamePlay;
    }

    public void OnContinue()
    {
        gameOverControl.SetActive(true);

        uiContinue.gameObject.SetActive(true);
        uiContinue.Enable();

        uiGameOver.Disable();

        state = GameState.Continue;
    }

    public void OnSecondChance()
    {
        gameOverControl.SetActive(false);

        uiContinue.Disable();

        uiGamePlay.gameObject.SetActive(true);
        uiGamePlay.Enable();

        state = GameState.GamePlay;
    }

    public void OnGameOver()
    {
        uiGamePlay.Disable();

        gameOverControl.SetActive(true);

        uiContinue.Disable();

        uiGameOver.gameObject.SetActive(true);
        uiGameOver.Enable();

        state = GameState.GameOver;
    }

    public void OnPause()
    {
        uiPaused.gameObject.SetActive(true);
        uiPaused.Enable();

        state = GameState.Paused;
    }

    public void OnResume()
    {
        uiPaused.Disable();

        state = GameState.GamePlay;
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
        ObjectPooler.Instance.RecallAll();
        Controller.Instance.RenewScene();

        flashImage.DOFade(targetAlpha, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            uiPaused.DisableImmediate();
            uiGamePlay.DisableImmediate();
            gameOverControl.SetActive(false);

            uiMainMenu.gameObject.SetActive(true);
            uiMainMenu.Enable();

            // renew scene
            Time.timeScale = 1f;
            DOTween.KillAll();
            state = GameState.MainMenu;

            flashImage.DOFade(0f, fadeDuration).SetUpdate(true);
        });
    }
}