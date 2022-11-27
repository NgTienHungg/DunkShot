using UnityEngine;
using System.Collections;

public class BasketSpawner : MonoBehaviour
{
    public Basket currentBasket, nextBasket;

    [SerializeField] private Vector2 rangeSpawnPosX;
    [SerializeField] private Vector2 rangeSpawnDistanceY;
    [SerializeField] private Vector2 rangeSpawnAngleZ;
    private float posX, disY, angleZ;
    private bool spawnInLeft;

    private void OnEnable()
    {
        GameEvent.GetScore += ChangeTargetBasket;
    }

    private void OnDisable()
    {
        GameEvent.GetScore -= ChangeTargetBasket;
    }

    private void Start()
    {
        Debug.Log("start spawner");

        currentBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();
        currentBasket.transform.position = new Vector3(-2f, -1f);

        spawnInLeft = false;
        SpawnNextBasket();

        // trong frame hiện tại: tất cả các hàm Start chưa được gọi xong
        // nếu set HasPoint luôn thì lúc sau lại bị Renew lại
        // => chờ hết frame này mới set
        StartCoroutine(PrepareFirstBasket());
    }

    private IEnumerator PrepareFirstBasket()
    {
        yield return new WaitForEndOfFrame();
        currentBasket.Point.SetHasPoint(false); // the first basket has no point
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
        spawnInLeft = !spawnInLeft;

        nextBasket.transform.position = new Vector3(posX, currentBasket.transform.position.y + disY);
        nextBasket.transform.eulerAngles = new Vector3(0f, 0f, angleZ);
    }

    private void ChangeTargetBasket()
    {
        Debug.Log("change basket");

        currentBasket.Disappear();
        currentBasket = nextBasket;

        SpawnNextBasket();
        nextBasket.Appear();
    }

    public Basket GetCurrentBasket()
    {
        return currentBasket;
    }

    private void OnDestroy()
    {
        currentBasket.Renew();
        ObjectPooler.Instance.Recall(currentBasket.gameObject);

        nextBasket.Renew();
        ObjectPooler.Instance.Recall(nextBasket.gameObject);
    }
}