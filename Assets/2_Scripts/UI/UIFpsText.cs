using TMPro;
using UnityEngine;

public class UIFpsText : MonoBehaviour
{
    private TextMeshProUGUI _fpsText;
    private float _timer;
    private string _prefix = "FPS: ";
    private int _fps;

    private void Awake()
    {
        _fpsText = GetComponent<TextMeshProUGUI>();
        _fps = Mathf.FloorToInt(1f / Time.deltaTime);
        _fpsText.text = _prefix + _fps.ToString();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            _fpsText.text = _prefix + "--";
            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= 0.6f)
        {
            _timer = 0f;
            _fps = Mathf.FloorToInt(1f / Time.deltaTime);
            _fpsText.text = _prefix + _fps.ToString();

            if (_fps <= 50) _fpsText.color = Color.red;
            else if (_fps <= 90) _fpsText.color = Color.yellow;
            else _fpsText.color = Color.green;
        }
    }
}