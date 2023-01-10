using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int Star { get; private set; }
    public int Token { get; private set; }

    [Header("Star")]
    [SerializeField] private GameObject _uiStar;
    [SerializeField] private TextMeshProUGUI _starCount;

    [Header("Token")]
    [SerializeField] private GameObject _uiToken;
    [SerializeField] private TextMeshProUGUI _tokenCount;

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
        _tokenCount.text = Token.ToString();

        ShowUIStar();
    }

    private void FixedUpdate()
    {
        _starCount.text = Star.ToString();
        _tokenCount.text = Token.ToString();
    }

    public void ShowUIStar()
    {
        _uiStar.SetActive(true);
        _uiToken.SetActive(false);
    }

    public void ShowUIToken()
    {
        _uiStar.SetActive(false);
        _uiToken.SetActive(true);
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
}