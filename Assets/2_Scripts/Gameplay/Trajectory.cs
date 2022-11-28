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
    [SerializeField] private GameObject dotParent;
    [SerializeField] private int numberOfDots;
    private Transform[] dots;

    private void Start()
    {
        CreateSimulationScene();
        PrepareDots();
        Hide();
    }

    private void CreateSimulationScene()
    {
        // scene
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene2D();

        // ball
        simulationBall = Instantiate(ballPrefab);
        simulationBall.GetComponent<SpriteRenderer>().enabled = false;
        ballRigidBody = simulationBall.GetComponent<Rigidbody2D>();
        SceneManager.MoveGameObjectToScene(simulationBall, simulationScene);

        // obstacles
        listObstacle = new List<GameObject>();
        foreach (Transform obj in obstacleParent)
        {
            GameObject simulationObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            simulationObj.GetComponent<SpriteRenderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(simulationObj, simulationScene);
            listObstacle.Add(simulationObj);
        }
    }

    private void PrepareDots()
    {
        dots = new Transform[numberOfDots];
        simulationBallPos = new Vector3[maxIterations];

        float currentScale = 1f;
        float scaleFactor = 0.05f;
        float minScale = 0.5f;

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = ObjectPooler.Instance.Spawn(ObjectTag.TrajectoryDot, dotParent.transform).transform;
            dots[i].localScale = Vector3.one * currentScale;

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
            physicsScene.Simulate(Time.fixedDeltaTime);
            simulationBallPos[i] = simulationBall.transform.position;
        }

        // draw trajectory
        for (int i = 0; i < numberOfDots; i++)
            dots[i].position = simulationBallPos[i * 3];
    }

    public void Show()
    {
        dotParent.SetActive(true);
    }

    public void Hide()
    {
        dotParent.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var dot in dots)
        {
            ObjectPooler.Instance.Recall(dot.gameObject);
        }
    }
}