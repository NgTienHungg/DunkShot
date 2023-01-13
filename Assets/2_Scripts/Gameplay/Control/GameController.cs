using UnityEngine;
using System.Collections;
using ES3Types;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private Mechanic _mechanic;
    public Mechanic Mechanic { get => _mechanic; }

    [SerializeField] private BasketSpawner _basketSpawner;
    public BasketSpawner BasketSpawner { get => _basketSpawner; }

    [SerializeField] private CamControl _camControl;
    public CamControl Camera { get => _camControl; }

    public bool IsPlaying { get; private set; }
    public bool HasSecondChance { get; private set; }

    private void Awake()
    {
        Instance = this;
        RegisterListener();
    }

    private void Start()
    {
        Observer.OnStartGame?.Invoke();
    }

    private void RegisterListener()
    {
        Observer.OnStartGame += StartGame;
        Observer.OnStartChallenge += StartChallenge;
        Observer.OnPlayChallenge += PlayChallenge;
        Observer.BallDead += OnBallDead;
    }

    private void StartGame()
    {
        CanvasController.Instance.Mode = GameMode.Endless;

        _basketSpawner.SpawnBasket();
        _mechanic.SetupBall();
        _camControl.SetupCamera();
        _camControl.FollowBall();

        PlayGame();
    }

    private void PlayGame()
    {
        this.IsPlaying = false;
        this.HasSecondChance = true;
    }

    private void StartChallenge()
    {
        CanvasController.Instance.Mode = GameMode.Challenge;
        ObjectPool.Instance.RecallAll();

        _basketSpawner.SetupLevel();
    }

    private void PlayChallenge()
    {
        _mechanic.SetupBall();
        _camControl.FollowBall();
    }

    private void Update()
    {
        if (CanvasController.Instance.State == GameState.MainMenu && Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
        {
            CanvasController.Instance.OnStartPlay();
            this.IsPlaying = true;
        }
    }

    private void OnBallDead()
    {
        Debug.Log("Controller: Ball Dead");
        this.IsPlaying = false;
        _camControl.UnfollowBall();
        StartCoroutine(WaitToHandle());
    }

    private void Restart()
    {
        _mechanic.SetupBall();
        _camControl.FollowBall();
        _basketSpawner.BasketReady();
        this.IsPlaying = true;
    }

    private IEnumerator WaitToHandle()
    {
        // chờ 0.25s rồi hiện các UI gameover
        yield return new WaitForSeconds(0.25f);
        ObjectPool.Instance.Recall(_mechanic.Ball.gameObject);

        if (ScoreManager.Instance.Score == 0)
            Restart();
        else if (ScoreManager.Instance.Score > 10 && this.HasSecondChance)
            Continue();
        else
            GameOver();
    }

    public void Continue()
    {
        CanvasController.Instance.OnContinue();
    }

    public void SecondChance()
    {
        Restart();
        this.HasSecondChance = false;
        CanvasController.Instance.OnSecondChance();
    }

    public void GameOver()
    {
        CanvasController.Instance.GameOver();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        IsPlaying = false;
        CanvasController.Instance.OnPause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPlaying = true;
        CanvasController.Instance.OnResume();
    }
}