﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private Mechanic _mechanic;
    public Mechanic Mechanic { get => _mechanic; }

    [SerializeField] private BasketControl _basketControl;
    public BasketControl BasketControl { get => _basketControl; }

    [SerializeField] private CameraControl _cameraControl;
    public CameraControl CameraControl { get => _cameraControl; }

    [SerializeField] private GameObject _prefab;
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

        Observer.OnStartChallenge += StartChallenge;
        Observer.OnPlayChallenge += PlayChallenge;
        Observer.OnCloseChallenge += CloseChallenge;
        Observer.BallDeadInChallenge += OnBallDeadInChallenge;
    }

    private void StartGame()
    {
        GameManager.Instance.Mode = GameMode.Endless;

        _basketControl.SpawnBasket();
        _mechanic.SetupBall();
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
    }

    private void OnBallDead()
    {
        this.IsPlaying = false;
        _cameraControl.UnfollowBall();
        StartCoroutine(WaitToHandle());
    }

    private void Restart()
    {
        _mechanic.SetupBall();
        _cameraControl.FollowBall();
        _basketControl.BasketReady();
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
        GameManager.Instance.State = GameState.Continue;
        CanvasController.Instance.OnContinue();
    }

    public void SecondChance()
    {
        Restart();
        this.HasSecondChance = false;
        GameManager.Instance.State = GameState.GamePlay;
        CanvasController.Instance.OnSecondChance();
    }

    public void GameOver()
    {
        GameManager.Instance.State = GameState.GameOver;
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

        DestroyImmediate(_level);
        _level = Instantiate(_prefab, transform.parent);
        _basketControl.SetupLevel();
    }

    private void PlayChallenge()
    {
        _mechanic.SetupBall();
        _cameraControl.FollowBall();
        this.IsPlaying = true;
    }

    private void CloseChallenge()
    {
        ObjectPool.Instance.RecallAll();
        DestroyImmediate(_level);
        StartGame();
    }

    private void OnBallDeadInChallenge()
    {
        Debug.LogWarning("Ball dead in challenge");
        this.IsPlaying = false;
        _cameraControl.UnfollowBall();
        StartCoroutine(WaitToHandleChallenge());
    }

    private IEnumerator WaitToHandleChallenge()
    {
        GameObject oldBall = _mechanic.Ball.gameObject;

        //else if (this.HasSecondChance)
        //    Continue();

        if (ScoreManager.Instance.Score == 0)
        {
            Restart();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            CanvasController.Instance.UIChallenge.CloseChallenge();
        }

        // chờ 0.5s để cho bóng biến mất
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.Recall(oldBall);
    }
}