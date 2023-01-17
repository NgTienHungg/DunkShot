using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int Star { get; private set; }
    public int Token { get; private set; }

    [Header("Star")]
    [SerializeField] private TextMeshProUGUI _starCount;
    [SerializeField] private TextMeshProUGUI _starCountCustomize;

    [Header("Token")]
    [SerializeField] private TextMeshProUGUI _tokenCountCustomize;
    [SerializeField] private TextMeshProUGUI _tokenCountChallenge;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Star = SaveSystem.GetInt(SaveKey.STAR);
        Token = SaveSystem.GetInt(SaveKey.TOKEN);

        _starCount.text = Star.ToString();
        _starCountCustomize.text = Star.ToString();

        _tokenCountCustomize.text = Token.ToString();
        _tokenCountChallenge.text = Token.ToString();
    }

    private void FixedUpdate()
    {
        _starCount.text = Star.ToString();
        _starCountCustomize.text = Star.ToString();

        _tokenCountCustomize.text = Token.ToString();
        _tokenCountChallenge.text = Token.ToString();
    }

    public bool CanBuyByStar(int price)
    {
        return Star >= price;
    }

    public void BuyByStar(int price)
    {
        Star -= price;
        SaveSystem.SetInt(SaveKey.STAR, Star);
    }

    public void AddStar(int amount)
    {
        Star += amount;
        SaveSystem.SetInt(SaveKey.STAR, Star);
    }

    public bool CanBuyByToken(int price)
    {
        return Token >= price;
    }

    public void BuyByToken(int price)
    {
        Token -= price;
        SaveSystem.SetInt(SaveKey.TOKEN, Token);
    }

    public void AddToken(int amount)
    {
        Token += amount;
        SaveSystem.SetInt(SaveKey.TOKEN, Token);
    }

    public void SpawnStar()
    {
        Vector3 basketPos = GameController.Instance.BasketSpawn.NextBasket.transform.position;
        float basketAngle = GameController.Instance.BasketSpawn.NextBasket.transform.eulerAngles.z;
        Star star = ObjectPool.Instance.Spawn(PoolTag.STAR).GetComponent<Star>();
        star.transform.position = basketPos + new Vector3(0.8f * Mathf.Sin(-basketAngle * Mathf.Deg2Rad), 0.8f);
        star.Appear();
    }

    public void SpawnToken()
    {
        Vector3 basketPos = GameController.Instance.BasketSpawn.NextBasket.transform.position;
        Token token = ObjectPool.Instance.Spawn(PoolTag.TOKEN).GetComponent<Token>();
        token.transform.position = basketPos + new Vector3(0f, 0.8f);
        token.Appear();
    }
}