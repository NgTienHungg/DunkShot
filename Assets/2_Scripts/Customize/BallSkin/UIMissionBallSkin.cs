using UnityEngine;
using UnityEngine.UI;

public class UIMissionBallSkin : UIBallSkin
{
    [Header("Mission")]
    [SerializeField] private Image _progress;

    protected override void Awake()
    {
        base.Awake();

        _ball.gameObject.SetActive(true);
    }

    public override void SetSkin(BallSkin skin)
    {
        base.SetSkin(skin);

        _progress.fillAmount = 1f * _skin.MissionProgress / _skin.Data.Mision.Target;
    }
}