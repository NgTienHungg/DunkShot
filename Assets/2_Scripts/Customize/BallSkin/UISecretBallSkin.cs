using TMPro;
using UnityEngine;

public class UISecretBallSkin : UIBallSkin
{
    [Header("Secret")]
    [SerializeField] private TextMeshProUGUI _index;

    public override void SetSkin(BallSkin skin)
    {
        base.SetSkin(skin);

        _index.text = (_skin.ID + 1).ToString();
    }
}