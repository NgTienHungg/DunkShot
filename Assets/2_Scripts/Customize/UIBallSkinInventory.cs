using UnityEngine;
using System.Collections.Generic;

public class UIBallSkinInventory : MonoBehaviour
{
    [Header("Trading Ball")]
    [SerializeField] private GameObject _uiTradingBallSkinPrefab;
    [SerializeField] private Transform _tradingBallContent;

    [Header("Video Ball")]
    [SerializeField] private GameObject _uiVideoBallSkinPrefab;
    [SerializeField] private Transform _videoBallContent;

    [Header("Mission Ball")]
    [SerializeField] private GameObject _uiMisisonBallSkinPrefab;
    [SerializeField] private Transform _missionBallContent;

    [Header("Challenge Ball")]
    [SerializeField] private GameObject _uiChallengeBallSkinPrefab;
    [SerializeField] private Transform _challengeBallContent;

    [Header("Secret Ball")]
    [SerializeField] private GameObject _uiSecretBallSkinPrefab;
    [SerializeField] private Transform _secretBallContent;

    [Header("Fortune Ball")]
    [SerializeField] private GameObject _uiFortuneBallSkinPrefab;
    [SerializeField] private Transform _fortuneBallContent;

    private List<UIBallSkin> _listBallSkin;

    private void Awake()
    {
        _listBallSkin = new List<UIBallSkin>();

        foreach (BallSkin skin in DataManager.Instance.BallSkins)
        {
            _listBallSkin.Add(CreateUIBallSkin(skin));
        }
    }

    private UIBallSkin CreateUIBallSkin(BallSkin ballSkin)
    {
        UIBallSkin uiBallSkin = null;

        switch (ballSkin.Data.Type)
        {
            case SkinType.TradingBall:
                uiBallSkin = Instantiate(_uiTradingBallSkinPrefab, _tradingBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.VideoBall:
                uiBallSkin = Instantiate(_uiVideoBallSkinPrefab, _videoBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.MissionBall:
                uiBallSkin = Instantiate(_uiMisisonBallSkinPrefab, _missionBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.ChallengeBall:
                uiBallSkin = Instantiate(_uiChallengeBallSkinPrefab, _challengeBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.SecretBall:
                uiBallSkin = Instantiate(_uiSecretBallSkinPrefab, _secretBallContent).GetComponent<UIBallSkin>();
                break;
            case SkinType.FortuneBall:
                uiBallSkin = Instantiate(_uiFortuneBallSkinPrefab, _fortuneBallContent).GetComponent<UIBallSkin>();
                break;
        }

        uiBallSkin.gameObject.name = ballSkin.Name;
        uiBallSkin.SetSkin(ballSkin);
        uiBallSkin.Renew();

        return uiBallSkin;
    }

    private void OnEnable()
    {
        Observer.ChangeBallSkin += ReloadInventory;
    }

    private void OnDisable()
    {
        Observer.ChangeBallSkin -= ReloadInventory;
    }

    private void ReloadInventory()
    {
        foreach (var uiBallSkin in _listBallSkin)
        {
            uiBallSkin.Renew();
        }
    }
}