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

    public static GameEvent OnUnlockSkin;
    public static GameEvent OnUnlockTheme;

    public delegate void SkinEvent(Skin skin);
    public static SkinEvent OnShowSkinPopup;

    public delegate void ThemeEvent(Theme theme);
    public static ThemeEvent OnShowThemePopup;

    public delegate void ChallengeEvent(ChallengeData challenge);
    public static ChallengeEvent OnPlayChallenge;
    public static ChallengeEvent OnPauseChallenge;
}