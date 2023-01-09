using TMPro;
using UnityEngine;

public class ChallengeSkinPopup : SkinPopup
{
    [SerializeField] private TextMeshProUGUI _description;

    public override void Show(Skin skin)
    {
        _description.text = "Complete\nNew Ball Challenge " + (skin.ID + 1).ToString();
    }
}