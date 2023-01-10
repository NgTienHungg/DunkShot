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

[RequireComponent(typeof(CanvasGroup))]
public class UIGame : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    public virtual void DisableImmediately()
    {
        gameObject.SetActive(false);
    }
}