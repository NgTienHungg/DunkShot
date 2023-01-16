using DG.Tweening;
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

    public void LoadChallenge()
    {
        _uiChallenge.LoadChallenge();
        _popupControl.LoadChallenge();
        StartChallenge();
    }

    public void StartChallenge()
    {
        _selection.SetActive(false);
        _uiChallenge.gameObject.SetActive(true);
        _popupControl.transform.DOScale(1f, 0f).SetDelay(0.4f).OnComplete(() =>
        {
            // chờ 1 lúc mới show popup play
            _popupControl.ShowPopupPlay();
        });

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
        Resume();
        _selection.SetActive(true);
        _uiChallenge.gameObject.SetActive(false);

        GameManager.Instance.Flash.ShowTransition();
        Observer.OnCloseChallenge?.Invoke();
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