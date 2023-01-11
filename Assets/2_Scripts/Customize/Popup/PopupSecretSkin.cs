using TMPro;
using UnityEngine;

public class PopupSecretSkin : PopupSkin
{
    [SerializeField] private TextMeshProUGUI _description;

    public override void Show(Skin skin)
    {
        base.Show(skin);
        _description.text = _skin.Data.Secret.Description;
    }
}