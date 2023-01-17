using DG.Tweening;
using UnityEngine;

public class UIChallengeManager : UIGame
{
    [SerializeField] private GameObject _selection;
    [SerializeField] private UIChallengeController _uiChallenge;
    [SerializeField] private PopupChallengeControl _popupControl;

    protected override void Awake()
    {
        base.Awake();
        RegisterListener();
    }

    private void RegisterListener()
    {
        Observer.OnPassChallenge += PassChallenge;
        Observer.OnFailChallenge += FailChallenge;
    }

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
        _popupControl.Disable();
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
        _popupControl.Disable();
    }

    public void ContinueChallenge()
    {
        //Debug.Log("CONTINUE");
        //Invoke(nameof(CloseChallenge), 1f);
    }

    private void FailChallenge()
    {
        _popupControl.ShowPopupFail();
    }

    private void PassChallenge()
    {
        ChallengeManager.Instance.PassChallenge();
        MoneyManager.Instance.AddToken(ChallengeManager.Instance.CurrentChallenge.NumberOfTokens);

        // chờ 1 lúc mới show popup
        transform.DOScale(1f, 1f).OnComplete(() =>
        {
            _popupControl.ShowPopupPass();
        });
    }

    public void RestartChallenge()
    {
        Time.timeScale = 1f;
        _popupControl.Disable();

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