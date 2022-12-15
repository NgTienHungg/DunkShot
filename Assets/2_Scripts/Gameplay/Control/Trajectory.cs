using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    [Header("Simulation")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform obstacleParent;
    private List<GameObject> listObstacle;

    [SerializeField] private int maxIterations;
    private Vector3[] simulationBallPos;

    private Scene simulationScene;
    private PhysicsScene2D physicsScene;
    private GameObject simulationBall;
    private Rigidbody2D ballRigidBody;

    [Header("Trajectory")]
    [SerializeField] private GameObject _dotPrefab;
    [SerializeField] private Transform _dotParent;
    [SerializeField] private int _dotCount;
    private Transform[] _dots;

    private void Start()
    {
        CreateSimulationScene();
        CreateSimulationBall();
        PrepareDots();
        Hide();
    }

    private void CreateSimulationScene()
    {
        // scene
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene2D();

        // obstacles
        listObstacle = new List<GameObject>();
        foreach (Transform obj in obstacleParent)
        {
            GameObject simulationObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            simulationObj.gameObject.tag = "Untagged";

            simulationObj.GetComponent<SpriteRenderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(simulationObj, simulationScene);
            listObstacle.Add(simulationObj);
        }
    }

    private void CreateSimulationBall()
    {
        simulationBall = Instantiate(ballPrefab);
        simulationBall.GetComponent<SpriteRenderer>().enabled = false;
        ballRigidBody = simulationBall.GetComponent<Rigidbody2D>();

        // remove all particle
        foreach (Transform child in simulationBall.transform)
            Destroy(child.gameObject);

        SceneManager.MoveGameObjectToScene(simulationBall, simulationScene);
    }

    public void PrepareDots()
    {
        _dots = new Transform[_dotCount];
        simulationBallPos = new Vector3[maxIterations];

        float currentScale = 1f;
        float scaleFactor = 0.05f;
        float minScale = 0.5f;

        for (int i = 0; i < _dotCount; i++)
        {
            _dots[i] = Instantiate(_dotPrefab, _dotParent).transform;
            _dots[i].localScale = Vector3.one * currentScale;

            if (currentScale > minScale)
                currentScale -= scaleFactor;
        }
    }

    public void Simulate(Vector3 firePoint, Vector3 force)
    {
        // Update obstacles position
        for (int i = 0; i < listObstacle.Count; i++)
            listObstacle[i].transform.position = obstacleParent.GetChild(i).position;

        // shoot ball
        simulationBall.transform.position = firePoint;
        ballRigidBody.velocity = Vector2.zero;
        ballRigidBody.AddForce(force, ForceMode2D.Impulse);

        // trajectory simulation
        for (var i = 0; i < maxIterations; i++)
        {
            //physicsScene.Simulate(Time.fixedDeltaTime);
            physicsScene.Simulate(0.01f);
            simulationBallPos[i] = simulationBall.transform.position;
        }

        // draw trajectory
        for (int i = 0; i < _dotCount; i++)
            _dots[i].position = simulationBallPos[5 + i * 5];
    }

    public void Show()
    {
        _dotParent.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _dotParent.gameObject.SetActive(false);
    }
}