using UnityEngine;

public class UICustomizeManager : UIGame
{
    protected override void OnEnable()
    {
    }

    public override void Enable()
    {
        gameObject.SetActive(true);
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