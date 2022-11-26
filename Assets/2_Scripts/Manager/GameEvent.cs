public static class GameEvent
{
    public delegate void AchievementEvent();

    public delegate void GameplayEvent();
    public static GameplayEvent PrepareBall;
    public static GameplayEvent ShootBall;
    public static GameplayEvent BasketReceiveBall;

    public static GameplayEvent GetScore;

}