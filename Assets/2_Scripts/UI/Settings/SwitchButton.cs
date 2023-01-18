using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    private enum SwitchButtonType
    {
        Sound,
        Vibrate,
        NightMode
    }

    [SerializeField] private SwitchButtonType _type;
    [SerializeField] private Image _button;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _activeColor, _inactiveColor;

    private string _keySave;
    private bool _enabled;

    private void Awake()
    {
        if (_type == SwitchButtonType.Sound)
        {
            _keySave = SaveKey.ON_SOUND;
            _enabled = SaveSystem.GetInt(_keySave) == 1;
        }
        else if (_type == SwitchButtonType.Vibrate)
        {
            _keySave = SaveKey.ON_VIBRATE;
            _enabled = SaveSystem.GetInt(_keySave) == 1;
        }
        else if (_type == SwitchButtonType.NightMode)
        {
            _keySave = SaveKey.ON_LIGHT_MODE;
            _enabled = SaveSystem.GetInt(_keySave) == 0;

            Observer.OnLightMode += OffButton;
            Observer.OnDarkMode += OnButton;
        }

        if (_enabled)
            OnButton();
        else
            OffButton();
    }

    private void OnButton()
    {
        _enabled = true;
        _button.color = _activeColor;
        _text.text = "ON";
    }

    private void OffButton()
    {
        _enabled = false;
        _button.color = _inactiveColor;
        _text.text = "OFF";
    }

    public void OnClick()
    {
        if (_enabled)
        {
            AudioManager.Instance.PlaySound(AudioKey.BUTTON_OFF);

            OffButton();
            if (_type == SwitchButtonType.NightMode)
            {
                // nếu đang ở mode tối -> set mode sáng thành true
                SaveSystem.SetInt(_keySave, 1); 
                Observer.OnLightMode?.Invoke();
            }
            else
                SaveSystem.SetInt(_keySave, 0);
        }
        else
        {
            OnButton();
            if (_type == SwitchButtonType.NightMode)
            {
                SaveSystem.SetInt(_keySave, 0);
                Observer.OnDarkMode?.Invoke();
            }
            else
                SaveSystem.SetInt(_keySave, 1);

            if (_type == SwitchButtonType.Vibrate)
                AudioManager.Instance.PlayVibrate();

            AudioManager.Instance.PlaySound(AudioKey.BUTTON_ON);
        }
    }
}