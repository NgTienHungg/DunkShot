using TMPro;
using UnityEngine;

public class UISecretSkin : UISkin
{
    [Header("Secret")]
    [SerializeField] private TextMeshProUGUI _index;

    public override void SetSkin(Skin skin)
    {
        base.SetSkin(skin);

        _index.text = (_skin.ID).ToString();
    }
}