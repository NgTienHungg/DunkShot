using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionSkinPopup : SkinPopup
{
    [SerializeField] private Image _ball;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _progress;
    [SerializeField] private Image _progressFill;

    public override void Show(Skin skin)
    {
        _skin = skin;
        _ball.sprite = _skin.Data.Sprite;
        _description.text = _skin.Data.Mision.Description.Replace("$", _skin.Data.Mision.Target.ToString());
        _progress.text = $"{_skin.MissionProgress}/{_skin.Data.Mision.Target}";
        _progressFill.fillAmount = 1f * _skin.MissionProgress / _skin.Data.Mision.Target;
    }

    public void OnTryButton()
    {
    }
}