using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    private Basket currentBasket, nextBasket;

    [SerializeField] private Vector2 rangeSpawnPosX;
    [SerializeField] private Vector2 rangeSpawnDistanceY;
    [SerializeField] private Vector2 rangeSpawnAngleZ;
    private float posX, disY, angleZ;
    private bool spawnInLeft;

    private void Start()
    {
        currentBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();
        currentBasket.transform.position = new Vector3(-2f, -1f);

        spawnInLeft = false;
        SpawnNextBasket();
    }

    private void SpawnNextBasket()
    {
        nextBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();

        if (!spawnInLeft)
        {
            posX = Random.Range(rangeSpawnPosX.x, rangeSpawnPosX.y);
            disY = Random.Range(rangeSpawnDistanceY.x, rangeSpawnDistanceY.y);
            angleZ = Random.Range(rangeSpawnAngleZ.x, rangeSpawnAngleZ.y);
        }
        else
        {
            posX = Random.Range(-rangeSpawnPosX.y, -rangeSpawnPosX.x);
            disY = Random.Range(rangeSpawnDistanceY.x, rangeSpawnDistanceY.y);
            angleZ = Random.Range(-rangeSpawnAngleZ.y, -rangeSpawnAngleZ.x);
        }

        nextBasket.transform.position = new Vector3(posX, currentBasket.transform.position.y + disY);
        nextBasket.transform.eulerAngles = new Vector3(0f, 0f, angleZ);
    }

    public Vector3 GetCurrentBasketPos()
    {
        return currentBasket.transform.position;
    }

    private void OnDestroy()
    {
        currentBasket.Renew();
        ObjectPooler.Instance.Recall(currentBasket.gameObject);

        nextBasket.Renew();
        ObjectPooler.Instance.Recall(nextBasket.gameObject);
    }
}