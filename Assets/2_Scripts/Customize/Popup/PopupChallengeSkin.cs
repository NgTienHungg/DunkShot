using TMPro;
using UnityEngine;

public class PopupChallengeSkin : PopupSkin
{
    [SerializeField] private TextMeshProUGUI _description;

    public override void Show(Skin skin)
    {
        _description.text = "Complete\nNew Ball Challenge " + (skin.ID).ToString();
    }
}