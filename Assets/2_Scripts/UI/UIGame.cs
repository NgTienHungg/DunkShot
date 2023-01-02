using UnityEngine;

public enum GameState
{
    MainMenu,
    GamePlay,
    Paused,
    Continue,
    GameOver,
    Settings,
    Customize,
}

public abstract class UIGame : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _canvasGroup;

    protected abstract void OnEnable();

    public abstract void Enable();

    public abstract void Disable();

    public abstract void DisableImmediately();
}