using UnityEngine;

public class PopupChallengeController : MonoBehaviour
{
    [SerializeField] private PopupPlayChallenge _popupPlay;
    [SerializeField] private PopupPauseChallenge _popupPause;
    //[SerializeField] private PopupContinueChallenge _popupContinue;

    public void LoadChallenge()
    {
        _popupPlay.Load();
        _popupPause.Load();
    }

    public void ShowPopupPlay()
    {
        gameObject.SetActive(true);
        _popupPlay.gameObject.SetActive(true);
    }

    public void ShowPopupPause()
    {
        gameObject.SetActive(true);
        _popupPause.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _popupPlay.gameObject.SetActive(false);
        _popupPause.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}