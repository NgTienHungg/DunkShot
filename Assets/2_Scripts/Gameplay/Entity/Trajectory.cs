using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Trajectory : MonoBehaviour
{
    [Header("Simulation")]
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private int maxIterations;

    private Scene simulationScene;
    private PhysicsScene2D physicsScene;
    private List<GameObject> listObstacle;

    [Header("Trajectory")]
    [SerializeField] private GameObject dotParent;
    [SerializeField] private int numberOfDots;

    private Transform[] dots;
    private Vector3[] simulationBallPos;

    private void Start()
    {
        CreatePhysicsScene();
        PrepareDots();
        Hide();
    }

    private void OnDestroy()
    {
        foreach (var dot in dots)
        {
            ObjectPooler.Instance.Recall(dot.gameObject);
        }
    }

    private void CreatePhysicsScene()
    {
        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene2D();

        listObstacle = new List<GameObject>();

        foreach (Transform obj in obstacleParent)
        {
            GameObject simulationObj = Instantiate(obj.gameObject, obj.position, obj.rotation);

            if (simulationObj.GetComponent<SpriteRenderer>())
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

        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = ObjectPooler.Instance.Spawn(ObjectTag.TrajectoryDot, dotParent.transform).transform;
            dots[i].localScale = Vector3.one * currentScale;

            if (currentScale > 0.5f)
                currentScale -= scaleFactor;
        }
    }

    public void Simulate(GameObject ballPrefab, Vector3 firePoint, Vector3 force)
    {
        GameObject simulationBall = Instantiate(ballPrefab, firePoint, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(simulationBall, simulationScene);

        simulationBall.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        for (var i = 0; i < maxIterations; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            simulationBallPos[i] = simulationBall.transform.position;
        }
        Destroy(simulationBall);

        // draw trajectory
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i].position = simulationBallPos[i * 3];
        }
    }

    public void Show()
    {
        dotParent.SetActive(true);
    }

    public void Hide()
    {
        dotParent.SetActive(false);
    }

    public bool IsShowing()
    {
        return dotParent.activeInHierarchy;
    }
}