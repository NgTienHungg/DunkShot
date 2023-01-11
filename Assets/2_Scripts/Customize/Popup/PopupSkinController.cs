using UnityEngine;

public class PopupSkinController : MonoBehaviour
{
    [SerializeField] private GameObject _missionSkinPopup;
    [SerializeField] private GameObject _challengeSkinPopup;
    [SerializeField] private GameObject _secretSkinPopup;
    [SerializeField] private GameObject _fortuneSkinPopup;

    private void Awake()
    {
        Observer.OnShowSkinPopup += ShowPopup;
    }

    private void Start()
    {
        _missionSkinPopup.SetActive(false);
        _challengeSkinPopup.SetActive(false);
        _secretSkinPopup.SetActive(false);
        _fortuneSkinPopup.SetActive(false);
    }

    private void ShowPopup(Skin skin)
    {
        switch (skin.Data.Type)
        {
            case SkinType.Normal:
                break;
            case SkinType.Video:
                break;
            case SkinType.Mission:
                _missionSkinPopup.SetActive(true);
                _missionSkinPopup.GetComponent<PopupMissionSkin>().Show(skin);
                break;
            case SkinType.Challenge:
                _challengeSkinPopup.SetActive(true);
                _challengeSkinPopup.GetComponent<PopupChallengeSkin>().Show(skin);
                break;
            case SkinType.Secret:
                _secretSkinPopup.SetActive(true);
                _secretSkinPopup.GetComponent<PopupSecretSkin>().Show(skin);
                break;
            case SkinType.Fortune:
                _fortuneSkinPopup.SetActive(true);
                break;
        }
    }
}