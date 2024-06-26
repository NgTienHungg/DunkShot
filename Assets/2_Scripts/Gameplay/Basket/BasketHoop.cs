using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [Header("Active")]
    [SerializeField] private SpriteRenderer _activeFrontSprite;
    [SerializeField] private SpriteRenderer _activeBackSprite;

    [Header("Inactive")]
    [SerializeField] private SpriteRenderer _inactiveFrontSprite;
    [SerializeField] private SpriteRenderer _inactiveBackSprite;

    [Header("Gold")]
    [SerializeField] private GameObject _blikas;

    private void Awake()
    {
        LoadTheme();
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnChangeTheme += LoadTheme;
    }

    private void LoadTheme()
    {
        _activeFrontSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.ActiveFront;
        _activeBackSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.ActiveBack;

        _inactiveFrontSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.InactiveFront;
        _inactiveBackSprite.sprite = DataManager.Instance.ThemeInUse.Data.Hoop.InactiveBack;
    }

    public void Renew()
    {
        _activeFrontSprite.gameObject.SetActive(true);
        _activeBackSprite.gameObject.SetActive(true);

        _inactiveFrontSprite.gameObject.SetActive(false);
        _inactiveBackSprite.gameObject.SetActive(false);

        _blikas.SetActive(false);
    }

    public void Inactive()
    {
        if (GetComponentInParent<Basket>().IsGolden)
        {
            return;
        }

        _activeFrontSprite.gameObject.SetActive(false);
        _activeBackSprite.gameObject.SetActive(false);

        _inactiveFrontSprite.gameObject.SetActive(true);
        _inactiveBackSprite.gameObject.SetActive(true);
    }

    public void SetGolden()
    {
        _blikas.SetActive(true);
    }
}