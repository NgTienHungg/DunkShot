using UnityEngine;
using UnityEngine.UI;

public class LightButton : MonoBehaviour
{
    [SerializeField] private Sprite _lightOnSprite, _lightOffSprite;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();

        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void OnClick()
    {
        if (SaveSystem.GetInt(SaveKey.ON_LIGHT_MODE) == 1)
        {
            TurnOff();
            Observer.OnDarkMode?.Invoke();
        }
        else
        {
            TurnOn();
            Observer.OnLightMode?.Invoke();
        }
    }

    private void TurnOn()
    {
        SaveSystem.SetInt(SaveKey.ON_LIGHT_MODE, 1);
        _image.sprite = _lightOnSprite;
    }

    private void TurnOff()
    {
        SaveSystem.SetInt(SaveKey.ON_LIGHT_MODE, 0);
        _image.sprite = _lightOffSprite;
    }
}