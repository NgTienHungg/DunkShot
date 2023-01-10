using UnityEngine;
using System.Collections.Generic;

public class SkinPage : MonoBehaviour
{
    [Header("Normal")]
    [SerializeField] private GameObject _uiNormalSkinPrefab;
    [SerializeField] private Transform _normalSkinContent;

    [Header("Video")]
    [SerializeField] private GameObject _uiVideoSkinPrefab;
    [SerializeField] private Transform _videoSkinContent;

    [Header("Mission")]
    [SerializeField] private GameObject _uiMisisonSkinPrefab;
    [SerializeField] private Transform _missionSkinContent;

    [Header("Challenge")]
    [SerializeField] private GameObject _uiChallengeSkinPrefab;
    [SerializeField] private Transform _challengeSkinContent;

    [Header("Secret")]
    [SerializeField] private GameObject _uiSecretSkinPrefab;
    [SerializeField] private Transform _secretSkinContent;

    [Header("Fortune")]
    [SerializeField] private GameObject _uiFortuneSkinPrefab;
    [SerializeField] private Transform _fortuneSkinContent;

    private List<UISkin> _listUiSkin;

    private void Awake()
    {
        _listUiSkin = new List<UISkin>();

        foreach (Skin skin in DataManager.Instance.Skins)
        {
            _listUiSkin.Add(CreateUIBallSkin(skin));
        }
    }

    private UISkin CreateUIBallSkin(Skin skin)
    {
        UISkin uiSkin = null;

        switch (skin.Data.Type)
        {
            case SkinType.Normal:
                uiSkin = Instantiate(_uiNormalSkinPrefab, _normalSkinContent).GetComponent<UISkin>();
                break;
            case SkinType.Video:
                uiSkin = Instantiate(_uiVideoSkinPrefab, _videoSkinContent).GetComponent<UISkin>();
                break;
            case SkinType.Mission:
                uiSkin = Instantiate(_uiMisisonSkinPrefab, _missionSkinContent).GetComponent<UISkin>();
                break;
            case SkinType.Challenge:
                uiSkin = Instantiate(_uiChallengeSkinPrefab, _challengeSkinContent).GetComponent<UISkin>();
                break;
            case SkinType.Secret:
                uiSkin = Instantiate(_uiSecretSkinPrefab, _secretSkinContent).GetComponent<UISkin>();
                break;
            case SkinType.Fortune:
                uiSkin = Instantiate(_uiFortuneSkinPrefab, _fortuneSkinContent).GetComponent<UISkin>();
                break;
        }

        uiSkin.gameObject.name = skin.Key;
        uiSkin.SetSkin(skin);

        return uiSkin;
    }

    private void OnEnable()
    {
        ReloadPage();

        Observer.ChangeSkin += ReloadPage;
    }

    private void OnDisable()
    {
        Observer.ChangeSkin -= ReloadPage;
    }

    private void ReloadPage()
    {
        foreach (var uiSkin in _listUiSkin)
        {
            uiSkin.Renew();
        }
    }
}