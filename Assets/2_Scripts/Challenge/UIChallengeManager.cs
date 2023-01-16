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
        _popupControl.ShowPopupPlay();

        GameManager.Instance.Flash.ShowTransition();
        Observer.OnStartChallenge?.Invoke();
    }

    public void PlayChallenge()
    {
        _popupControl.DisablePopupPlay();
        Observer.OnPlayChallenge?.Invoke();
    }

    public void PauseChallenge()
    {
        Time.timeScale = 0f;
        _popupControl.ShowPopupPause();
    }

    public void ResumeChallenge()
    {
        Time.timeScale = 1f;
        _popupControl.DisablePopupPause();
    }

    public void PassChallenge()
    {
        ChallengeManager.Instance.PassChallenge();

        // chờ 1 lúc mới show popup
        transform.DOScale(1f, 1f).OnComplete(() =>
        {
            _popupControl.ShowPopupPass();
        });
    }

    public void RestartChallenge()
    {
        Time.timeScale = 1f;
        _popupControl.DisablePopupPause();

        GameManager.Instance.Flash.ShowTransition();
        Observer.OnStartGame?.Invoke();
        Observer.OnRestartChallenge?.Invoke();
    }

    public void CloseChallenge()
    {
        _popupControl.Disable();
        _selection.SetActive(true);
        _uiChallenge.gameObject.SetActive(false);

        Time.timeScale = 1f;
        GameManager.Instance.Flash.ShowTransition();
        Observer.OnCloseChallenge?.Invoke();
    }

    public void OnBackButton()
    {
        CanvasController.Instance.CloseCustomize();
    }
}