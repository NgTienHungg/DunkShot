using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }
    public Mechanic Mechanic { get; private set; }
    public BasketSpawner BasketSpawner { get; private set; }
    public bool IsPlaying { get; private set; }
    public bool HasSecondChance { get; private set; }

    public void Renew()
    {
        CameraController.Instance.Renew();
        Background.Instance.Renew();
        ScoreManager.Instance.Renew();

        BasketSpawner.Renew();
        Mechanic.Renew();

        this.IsPlaying = false;
        this.HasSecondChance = true;
    }

    private void Awake()
    {
        Instance = this;
        Mechanic = FindObjectOfType<Mechanic>();
        BasketSpawner = FindObjectOfType<BasketSpawner>();
    }

    private void OnEnable()
    {
        Observer.BallDead += OnBallDead;
    }

    private void OnDisable()
    {
        Observer.BallDead -= OnBallDead;
    }

    private void Start()
    {
        Renew();
    }

    private void Update()
    {
        if (UIManager.Instance.State == GameState.MainMenu && Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
        {
            UIManager.Instance.OnStartPlay();
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
        BasketSpawner.PreparePlay();
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
        UIManager.Instance.OnContinue();
    }

    public void SecondChance()
    {
        Restart();
        HasSecondChance = false;
        UIManager.Instance.OnSecondChance();
    }

    public void GameOver()
    {
        UIManager.Instance.OnGameOver();
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        IsPlaying = false;

        UIManager.Instance.OnPause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        IsPlaying = true;

        UIManager.Instance.OnResume();
    }

    //public void Reload()
    //{
    //    DOTween.KillAll();
    //    ObjectPooler.Instance.RecallAll();
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene(0);
    //}
}