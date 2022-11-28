public static class Observer
{
    public delegate void AchievementEvent();

    public delegate void GameEvent();
    public static GameEvent BasketReceiveBall;
    public static GameEvent GetScore;
    public static GameEvent BallDead;
}