using UnityEngine;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }
    [ShowInInspector] public Mechanic Mechanic { get; private set; }
    [ShowInInspector] public BasketSpawner BasketSpawner { get; private set; }
    public Ball Ball { get; private set; }
    public bool IsPlaying { get; private set; }
    public bool HasSecondChance { get; private set; }

    private void Awake()
    {
        Instance = this;
        RegisterListener();
        //Mechanic = FindObjectOfType<Mechanic>();
        //BasketSpawner = FindObjectOfType<BasketSpawner>();
    }

    private void RegisterListener()
    {
        Observer.BallDead += OnBallDead;
        //Observer.OnStartChallenge += Renew;
        Observer.OnPlayChallenge += Renew;
    }

    public void Renew()
    {
        CameraController.Instance.Renew();
        Background.Instance.Renew();
        //ScoreManager.Instance.Renew();

        //BasketSpawner.Renew();
        Mechanic.Renew();

        this.IsPlaying = false;
        this.HasSecondChance = true;
    }

    private void OnDisable()
    {
        Observer.BallDead -= OnBallDead;
        Observer.OnPlayChallenge -= Renew;
    }

    private void Start()
    {
        Observer.OnStartGame?.Invoke();
        //Observer.RenewScene?.Invoke();
        Renew();
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
        StartCoroutine(WaitToRestart());
    }

    private void Restart()
    {
        Mechanic.Renew();
        BasketSpawner.CurrentBasket.transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
        IsPlaying = true;
    }

    private IEnumerator WaitToRestart()
    {
        this.IsPlaying = false;

        yield return new WaitForSeconds(0.25f);

        ObjectPool.Instance.Recall(Mechanic.GetBall().gameObject);

        // restart
        if (ScoreManager.Instance.Score == 0)
            Restart();
        else if (ScoreManager.Instance.Score > 10 && HasSecondChance)
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
        HasSecondChance = false;
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