using UnityEngine;

public class UICustomizeManager : UIGame
{
    protected override void OnEnable()
    {
        Debug.Log("UI customize: on enable");
    }

    public override void Enable()
    {
        Debug.Log("UI customize: enable");
        gameObject.SetActive(true);
        Debug.Log("UI customize: enable2");

    }

    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public override void DisableImmediately()
    {
    }

    public void OnBackButton()
    {
        // audio
        UIManager.Instance.CloseCustomize();
    }
}