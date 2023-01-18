using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private BallShootingControl _ballShooting;
    public BallShootingControl BallShooting { get => _ballShooting; }

    [SerializeField] private BasketSpawnControl _basketSpawn;
    public BasketSpawnControl BasketSpawn { get => _basketSpawn; }

    [SerializeField] private CameraControl _cameraControl;
    public CameraControl CameraControl { get => _cameraControl; }

    [SerializeField] private GameObject _level;
    public GameObject Level { get => _level; }

    public bool IsPlaying { get; private set; }
    public bool HasSecondChance { get; private set; }

    private void Awake()
    {
        Instance = this;
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnStartGame += StartGame;
        Observer.OnPlayGame += PlayGame;
        Observer.BallDead += OnBallDead;
        Observer.BallStuck += OnBallStuck;

        Observer.OnStartChallenge += StartChallenge;
        Observer.OnPlayChallenge += PlayChallenge;
        Observer.OnRestartChallenge += RestartChallenge;
        Observer.OnCloseChallenge += CloseChallenge;

        Observer.BallDeadInChallenge += BallDeadInChallenge;
        Observer.BallRebornInChallenge += BallRebornInChallenge;
        Observer.FreeBallRebornInChallenge += Restart;
    }


    //// làm vầy để sau khi chạy xong hiệu ứng mở app và sinh ra vCam không bị giật
    private void Start()
    {
        _cameraControl.SetupCamera();
    }

    public void OpenApp()
    {
        GameManager.Instance.Mode = GameMode.Endless;

        _basketSpawn.SpawnBasket();
        _ballShooting.SetupBall();
        _cameraControl.FollowBall();

        this.IsPlaying = false;
        this.HasSecondChance = true;
    }

    private void StartGame()
    {
        GameManager.Instance.Mode = GameMode.Endless;

        _basketSpawn.SpawnBasket();
        _ballShooting.SetupBall();
        _cameraControl.SetupCamera();
        _cameraControl.FollowBall();

        this.IsPlaying = false;
        this.HasSecondChance = true;
    }

    private void PlayGame()
    {
        this.IsPlaying = true;
        GameManager.Instance.State = GameState.GamePlay;
        ScoreManager.Instance.UIScore.Show();
        CanvasController.Instance.StartPlay();
        Observer.PlayGame?.Invoke();
    }

    private void OnBallDead()
    {
        this.IsPlaying = false;
        _cameraControl.UnfollowBall();
        StartCoroutine(WaitToHandle());
    }

    private void OnBallStuck()
    {
        CanvasController.Instance.ShowStuck();
    }

    private void Restart()
    {
        _ballShooting.SetupBall();
        _cameraControl.FollowBall();
        _basketSpawn.BasketReady();
        this.IsPlaying = true;
    }

    private IEnumerator WaitToHandle()
    {
        // chờ 0.25s rồi hiện các UI gameover
        yield return new WaitForSeconds(0.25f);
        ObjectPool.Instance.Recall(_ballShooting.Ball.gameObject);

        if (ScoreManager.Instance.Score == 0)
            Restart();
        else if (ScoreManager.Instance.Score > 10 && this.HasSecondChance)
            Continue();
        else
            GameOver();
    }

    public void Continue()
    {
        GameManager.Instance.State = GameState.Continue;
        CanvasController.Instance.OnContinue();
    }

    public void SecondChance()
    {
        Restart();
        this.HasSecondChance = false;
        GameManager.Instance.State = GameState.GamePlay;
        CanvasController.Instance.OnSecondChance();
        Observer.ContinueGame?.Invoke();
    }

    public void GameOver()
    {
        GameManager.Instance.State = GameState.GameOver;
        AudioManager.Instance.PlaySound(AudioKey.GAMEOVER);
        CanvasController.Instance.GameOver();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        this.IsPlaying = false;
        GameManager.Instance.State = GameState.Paused;
        CanvasController.Instance.OnPause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        this.IsPlaying = true;
        GameManager.Instance.State = GameState.GamePlay;
        CanvasController.Instance.OnResume();
    }

    /*---------------------------------------------*/
    private void StartChallenge()
    {
        GameManager.Instance.Mode = GameMode.Challenge;
        ObjectPool.Instance.RecallAll();

        if (_level != null)
        {
            foreach (Transform child in _level.transform)
            {
                // tránh Warning của DOTWeen
                child.DOKill();
            }
            DestroyImmediate(_level);
        }

        _level = Instantiate(ChallengeManager.Instance.CurrentChallenge.Level, transform.parent);
        _basketSpawn.SetupLevel();
    }

    private void PlayChallenge()
    {
        _ballShooting.SetupBall();
        _cameraControl.FollowBall();
        this.IsPlaying = true;
    }

    private void RestartChallenge()
    {
        StartChallenge();
        PlayChallenge();
    }

    private void CloseChallenge()
    {
        ObjectPool.Instance.RecallAll();
        DestroyImmediate(_level);
        StartGame();
    }

    private void BallDeadInChallenge()
    {
        this.IsPlaying = false;
        _cameraControl.UnfollowBall();
        ObjectPool.Instance.Recall(_ballShooting.Ball.gameObject);
    }

    private void BallRebornInChallenge()
    {
        _ballShooting.SetupBall();
        _cameraControl.FollowBall();
        _basketSpawn.BasketReady();
        this.IsPlaying = true;
    }
}