public static class Observer
{
    public delegate void GameEvent();

    public static GameEvent OnStartGame;
    public static GameEvent OnPlayGame;

    public static GameEvent BallDead;
    public static GameEvent BallSmoke;
    public static GameEvent BallFlame;
    public static GameEvent BallCollideHoop;
    public static GameEvent BallCollideObstacle;
    public static GameEvent BallInBasket;
    public static GameEvent BallInBasketHasPoint;

    public static GameEvent OnStartChallenge;
    public static GameEvent OnPlayChallenge;
    public static GameEvent OnPassChallenge;
    public static GameEvent OnCloseChallenge;

    public static GameEvent BallDeadInChallenge;
    public static GameEvent BallInBasketInChallenge;
    public static GameEvent BallInBasketHasPointInChallenge;

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
    public static ChallengeEvent OnPauseChallenge;
}