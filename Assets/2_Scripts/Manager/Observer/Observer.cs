public static class Observer
{
    public delegate void AchievementEvent();

    public delegate void GameEvent();
    public static GameEvent BallInBasket;
    public static GameEvent BallCollideObstacle;
    public static GameEvent BallCollideHoop;
    public static GameEvent GetScore;
    public static GameEvent BallDead;

    public static GameEvent BallSmoke;
    public static GameEvent BallFlame;

    public static GameEvent OnLightMode;
    public static GameEvent OnDarkMode;

    public static GameEvent OnChangeSkin;
    public static GameEvent OnChangeTheme;

    public delegate void SkinEvent(Skin skin);
    public static SkinEvent OnShowSkinPopup;
    public static SkinEvent OnUnlockSkin;

    public delegate void ThemeEvent(Theme theme);
    public static ThemeEvent OnShowThemePopup;
    
}