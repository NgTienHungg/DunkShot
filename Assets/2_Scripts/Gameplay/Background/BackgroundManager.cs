using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance { get; private set; }

    [Header("Background")]
    [SerializeField] private GameObject darkMode;
    [SerializeField] private GameObject lightMode;

    [Header("Scroll")]
    [SerializeField] private ScrollBackground clouds;
    [SerializeField] private ScrollBackground stars;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);
    }

    public void Renew()
    {
        clouds.Renew();
        stars.Renew();

        // ??t ? ?ây vì n?u setActive of cho darkMode thì s? không set up ???c vào scroll bg c?a DarkMode
        if (SaveManager.Instance.GetInt("OnLightMode") == 1)
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