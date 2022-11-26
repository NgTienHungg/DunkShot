using UnityEngine;
using Cinemachine;
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
    public CinemachineVirtualCamera cinemachine;

    private Ball ball;

    private void Start()
    {
        Debug.Log("start controller");

        Vector3 pos = basketSpawner.GetCurrentBasket().transform.position;
        ball = ObjectPooler.Instance.Spawn(ObjectTag.Ball).GetComponent<Ball>();
        ball.transform.position = new Vector3(pos.x, pos.y + 1f);

        mechanic.SetBall(ball);
        cinemachine.Follow = ball.transform;
    }

    private void Update()
    {
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        ball.Renew();
        ObjectPooler.Instance.Recall(ball.gameObject);
    }
}