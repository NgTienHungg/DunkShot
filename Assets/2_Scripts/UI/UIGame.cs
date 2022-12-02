using UnityEngine;

public abstract class UIGame : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    public abstract void Enable();

    public abstract void Disable();
}