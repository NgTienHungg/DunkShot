using UnityEngine;

public class UIBallSkinInventory : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _prefab;

    [Header("Contents")]
    [SerializeField] private Transform _tradingBallContent;
    [SerializeField] private Transform _videoBallContent;
    [SerializeField] private Transform _missionBallContent;
    [SerializeField] private Transform _challengeBallContent;
    [SerializeField] private Transform _secretBallContent;
    [SerializeField] private Transform _fortuneBallContent;

    private void Awake()
    {
        foreach (BallSkin skin in DataManager.Instance.BallSkins)
        {
            CreateUIBallSkin(skin);
        }
    }

    private void CreateUIBallSkin(BallSkin ballSkin)
    {
        UIBallSkin uiBallSkin = null;

        switch (ballSkin.Data.Type)
        {
            case SkinType.TradingBall:
                uiBallSkin = Instantiate(_prefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.VideoBall:
                uiBallSkin = Instantiate(_prefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.MissionBall:
                uiBallSkin = Instantiate(_prefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.ChallengeBall:
                uiBallSkin = Instantiate(_prefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.SecretBall:
                uiBallSkin = Instantiate(_prefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.FortuneBall:
                uiBallSkin = Instantiate(_prefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
        }

        uiBallSkin.gameObject.name = ballSkin.Name;
        uiBallSkin.SetSkin(ballSkin);
    }
}