using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private static BackgroundManager _instance;
    public static BackgroundManager Instance { get => _instance; }

    [Header("Background")]
    [SerializeField] private GameObject darkMode;
    [SerializeField] private GameObject lightMode;

    [Header("Scroll")]
    [SerializeField] private ScrollBackground clouds;
    [SerializeField] private ScrollBackground stars;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void Renew()
    {
        clouds.Renew();
        stars.Renew();

        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
            OnLightMode();
        else
            OnDarkMode();
    }

    public void OnLightMode()
    {
        lightMode.SetActive(true);
        darkMode.SetActive(false);
    }

    public void OnDarkMode()
    {
        lightMode.SetActive(false);
        darkMode.SetActive(true);
    }
}