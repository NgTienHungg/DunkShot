public class CustomizeManager : UIGame
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

    public void OnVideoButton()
    {
        // audio
    }

    public void OnSkinButton()
    {
        // audio
    }

    public void OnThemeButton()
    {
        // audio
    }
}