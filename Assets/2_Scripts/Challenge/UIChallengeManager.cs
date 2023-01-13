using UnityEngine;

public class UIChallengeManager : UIGame
{
    [SerializeField] private GameObject _selection;
    [SerializeField] private UIChallengeController _uiChallenge;
    [SerializeField] private PopupChallengeController _popupControl;

    private void OnEnable()
    {
        _selection.SetActive(true);
        _uiChallenge.gameObject.SetActive(false);
        _popupControl.Disable();
    }

    public void LoadChallenge(ChallengeData challenge)
    {
        _uiChallenge.LoadChallenge(challenge);
        _popupControl.LoadChallenge(challenge);
        StartChallenge();
    }

    public void StartChallenge()
    {
        _selection.SetActive(false);
        _uiChallenge.gameObject.SetActive(true);
        _popupControl.ShowPopupPlay();

        ObjectPool.Instance.RecallAll();
        CanvasController.Instance.Mode = GameMode.Challenge;
        GameManager.Instance.Flash.ShowTransition();
        Observer.OnStartChallenge?.Invoke();
    }

    public void PlayChallenge()
    {
        _popupControl.Disable();
        Observer.OnPlayChallenge?.Invoke();
    }

    public void CloseChallenge()
    {
        _selection.SetActive(true);
        _uiChallenge.gameObject.SetActive(false);
        _popupControl.Disable();
    }

    public void ClosePopup()
    {
        _popupControl.Disable();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _popupControl.ShowPopupPause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        _popupControl.Disable();
    }

    public void Restart()
    {
        Debug.Log("RESTART");
    }

    public void OnBackButton()
    {
        CanvasController.Instance.CloseCustomize();
    }
}