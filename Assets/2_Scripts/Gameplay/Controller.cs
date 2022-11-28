using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    #region Singleton
    public static Controller Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Mechanic mechanic;
    public BasketSpawner basketSpawner;

    //[HideInInspector]
    public bool IsPlaying;
    private bool hasSecondChance;

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
        Debug.Log("Start in controller");

        mechanic.SetBasket(basketSpawner.GetCurrentBasket());
        mechanic.PrepareBall();

        this.IsPlaying = false;
        this.hasSecondChance = true;
    }

    private void Update()
    {
        if (UIManager.Instance.state == GameState.MainMenu && Input.GetMouseButtonDown(0) && !Util.IsPointerOverUIObject())
        {
            Debug.Log("Update control");
            IsPlaying = true;
            UIManager.Instance.OnStartPlay();
        }
    }

    private void OnBallDead()
    {
        Debug.Log("Controller: Ball Dead");
        StartCoroutine(WaitToRestart());
    }

    private void Restart()
    {
        mechanic.PrepareBall();
        basketSpawner.GetCurrentBasket().transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
        IsPlaying = true;
    }

    private IEnumerator WaitToRestart()
    {
        this.IsPlaying = false;

        yield return new WaitForSeconds(0.2f);

        Ball.Recall(mechanic.GetBall());

        // restart
        if (ScoreManager.Instance.Score == 0 || hasSecondChance)
        {
            hasSecondChance = false;
            Restart();
        }
        else
        {
            UIManager.Instance.OnGameOver();
        }
    }

    public void Reload()
    {
        DOTween.KillAll();
        ObjectPooler.Instance.RecallAll();
        SceneManager.LoadScene(0);
    }
}