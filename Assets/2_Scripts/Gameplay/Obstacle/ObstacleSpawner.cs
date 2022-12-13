using DG.Tweening;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    private Basket _basket;
    private bool _inTheRight;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn(Basket basket)
    {
        _basket = basket;
        _inTheRight = basket.transform.position.x > 0f;

        if (Mathf.Abs(basket.transform.position.x) >= 2.5f)
        {
            int rnd = Random.Range(0, 2);

            if (rnd == 1)
                SpawnRotateBar();
            else
                SpawnHorizontalBar();
        }
        else
        {
            int rnd = Random.Range(0, 2);

            if (rnd == 1)
                SpawnTopBar();
            else
                SpawnBesideBar();
        }
    }

    private void SpawnBesideBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(ObjectTag.BesideBar).GetComponent<Obstacle>();

        float x = Random.Range(1.35f, 1.55f);
        float y = Random.Range(0.6f, 0.85f);
        float dir = _inTheRight ? 1 : -1;

        obstacle.transform.position = _basket.transform.position + new Vector3(dir * x, y);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnTopBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(ObjectTag.TopBar).GetComponent<Obstacle>();

        float y = Random.Range(2.4f, 3f);

        obstacle.transform.position = _basket.transform.position + new Vector3(0, y);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnHorizontalBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(ObjectTag.HorizontalBar).GetComponent<Obstacle>();

        float x = Random.Range(3f, 4f);
        float dir = _inTheRight ? -1 : 1;

        obstacle.transform.position = _basket.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnRotateBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(ObjectTag.RotationBar).GetComponent<Obstacle>();

        float x = Random.Range(3.5f, 4.2f);
        float y = Random.Range(-1f, -0.2f);
        float dir = _inTheRight ? -1 : 1;

        obstacle.transform.position = _basket.transform.position + new Vector3(dir * x, y);
        obstacle.transform.DORotate(Vector3.forward * 360, 2.6f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }
}