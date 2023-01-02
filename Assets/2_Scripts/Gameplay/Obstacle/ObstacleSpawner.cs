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

        int id = Random.Range(1, 10);

        switch (id)
        {
            case 1:
                SpawnBesideBar();
                break;
            case 2:
                SpawnTopBar();
                break;
            case 3:
                SpawnHorizontalBar();
                break;
            case 4:
                SpawnRotateBar();
                break;
            case 5:
                SpawnShield();
                break;
            case 6:
                SpawnTopBackboard();
                break;
            case 7:
                SpawnSingleBesideBackboard();
                break;
            //case 8:
            //    SpawnTwoBesideBackboard();
            //    break;
            default:
                break;
        }
    }

    private void SpawnBesideBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BAR_4).GetComponent<Obstacle>();

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
        Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BAR_3).GetComponent<Obstacle>();

        float y = Random.Range(2.4f, 3f);

        obstacle.transform.position = _basket.transform.position + new Vector3(0, y);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnHorizontalBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BAR_1).GetComponent<Obstacle>();

        float x = Random.Range(3f, 4f);
        float dir = _inTheRight ? -1 : 1;

        obstacle.transform.position = _basket.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnRotateBar()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BAR_2).GetComponent<Obstacle>();

        float x = Random.Range(3.2f, 3.5f);
        float y = Random.Range(-0.5f, -0.2f);
        float duration = Random.Range(2.6f, 3f);
        float dir = _inTheRight ? -1 : 1;

        obstacle.transform.position = _basket.transform.position + new Vector3(dir * x, y);
        obstacle.transform.DORotate(Vector3.forward * 360, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnShield()
    {
        // random shield
        string obstacleTag = "Shield" + Random.Range(1, 5);
        Obstacle obstacle = ObjectPooler.Instance.Spawn(obstacleTag).GetComponent<Obstacle>();

        float duration = Random.Range(2.8f, 4f);

        obstacle.transform.position = _basket.transform.position;
        obstacle.transform.DORotate(Vector3.forward * 360f, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
    }

    private void SpawnTopBackboard()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BACK_BOARD).GetComponent<Obstacle>();

        float y = Random.Range(2.6f, 3f);

        obstacle.transform.position = _basket.transform.position + new Vector3(0, y);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    private void SpawnSingleBesideBackboard()
    {
        Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BACK_BOARD).GetComponent<Obstacle>();

        float x = Random.Range(1.85f, 2.1f);
        float dir = _inTheRight ? -1 : 1;

        obstacle.transform.position = _basket.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        _basket.Obstacle.Add(obstacle);
        _basket.transform.rotation = Quaternion.identity;
    }

    //private void SpawnTwoBesideBackboard()
    //{
    //    Obstacle obstacle = ObjectPooler.Instance.Spawn(PoolTag.BesideBackboard).GetComponent<Obstacle>();

    //    float x = Random.Range(2f, 2.2f);
    //    float y = _basket.transform.position.y;
    //    float dir = _inTheRight ? 1 : -1;

    //    _basket.transform.position = new Vector3(dir * x, y);
    //    obstacle.transform.position = _basket.transform.position;
    //    obstacle.Appear();

    //    _basket.Obstacle.Add(obstacle);
    //    _basket.transform.rotation = Quaternion.identity;
    //}
}