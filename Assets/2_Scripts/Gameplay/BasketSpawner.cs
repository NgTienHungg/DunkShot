using DG.Tweening;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    private Basket currentBasket, nextBasket;

    [Header("Set up")]
    [SerializeField] private Vector2 firstBasketPos;
    [SerializeField] private Vector2 secondBasketPos;

    [Header("Spawn")]
    [SerializeField] private Vector2 rangeSpawnPosX;
    [SerializeField] private Vector2 rangeSpawnDistanceY;
    [SerializeField] private Vector2 rangeSpawnAngleZ;
    private float posX, disY, angleZ;
    private bool spawnInLeft;

    public void Renew()
    {
        currentBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();
        currentBasket.transform.position = firstBasketPos;
        currentBasket.Point.SetHasPoint(false); // the first basket has no point

        nextBasket = ObjectPooler.Instance.Spawn(ObjectTag.Basket).GetComponent<Basket>();
        nextBasket.transform.position = secondBasketPos;

        spawnInLeft = true;
    }

    private void OnEnable()
    {
        Observer.GetScore += ChangeTargetBasket;
    }

    private void OnDisable()
    {
        Observer.GetScore -= ChangeTargetBasket;
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
        Debug.Log("Change basket");

        currentBasket.Disappear();
        currentBasket = nextBasket;

        SpawnNextBasket();
        nextBasket.Appear();
    }

    public Basket GetCurrentBasket()
    {
        return currentBasket;
    }

    public void PreparePlay()
    {
        currentBasket.transform.DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
    }
}