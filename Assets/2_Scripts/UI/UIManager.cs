using UnityEngine;

public enum GameState
{
    MainMenu,
    GamePlay,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIGamePlay uiGamePlay;
    [SerializeField] private UIGameOver uiGameOver;

    public static UIManager Instance { get; private set; }

    public GameState state;

    private void Awake()
    {
        UIManager.Instance = this;

        uiMainMenu.gameObject.SetActive(true);
        uiGamePlay.gameObject.SetActive(false);
        uiGameOver.gameObject.SetActive(false);

        this.state = GameState.MainMenu;
    }

    public void OnStartPlay()
    {
        Debug.Log("UI Manager: Start play");

        uiMainMenu.Disable();
        
        uiGamePlay.gameObject.SetActive(true);
        uiGamePlay.Enable();

        this.state = GameState.GamePlay;
    }

    public void OnGameOver()
    {
        Debug.Log("UI Manager: Game over");

        uiGamePlay.Disable();

        uiGameOver.gameObject.SetActive(true);
        uiGameOver.Enable();

        this.state = GameState.GameOver;
    }
}