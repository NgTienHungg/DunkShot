using UnityEngine;

public abstract class UIGame : MonoBehaviour
{
    protected CanvasGroup canvasGroup;

    protected void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public abstract void Enable();

    public abstract void Disable();
}