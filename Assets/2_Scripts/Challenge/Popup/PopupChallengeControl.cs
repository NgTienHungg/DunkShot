using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupChallengeControl : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private PopupPlayChallenge _popupPlay;
    [SerializeField] private PopupPauseChallenge _popupPause;
    [SerializeField] private PopupPassChallenge _popupPass;
    [SerializeField] private PopupFailChallenge _popupFail;

    private void OnEnable()
    {
        _popupPlay.gameObject.SetActive(false);
        _popupPause.gameObject.SetActive(false);
        _popupPass.gameObject.SetActive(false);
        _popupFail.gameObject.SetActive(false);

        _panel.enabled = false;
        _panel.DOFade(0f, 0f);
    }

    public void LoadChallenge()
    {
        _popupPlay.Load();
        _popupPause.Load();
        _popupPass.Load();
        _popupFail.Load();
    }

    public void ShowPopupPlay()
    {
        AudioManager.Instance.PlaySound(AudioKey.POPUP_APPEAR);
        _panel.enabled = true;
        _panel.DOFade(0.4f, 0.5f).OnComplete(() =>
        {
            _popupPlay.gameObject.SetActive(true);
        });
    }

    public void ShowPopupPause()
    {
        AudioManager.Instance.PlaySound(AudioKey.POPUP_APPEAR);
        _panel.enabled = true;
        _panel.DOFade(0.4f, 0.5f).SetUpdate(true);
        _popupPause.gameObject.SetActive(true);
    }

    public void ShowPopupPass()
    {
        AudioManager.Instance.PlaySound(AudioKey.POPUP_APPEAR);
        _panel.enabled = true;
        _panel.DOFade(0.4f, 0.5f).OnComplete(() =>
        {
            _popupPass.gameObject.SetActive(true);
        });
    }

    public void ShowPopupFail()
    {
        AudioManager.Instance.PlaySound(AudioKey.POPUP_APPEAR);
        _panel.enabled = true;
        _panel.DOFade(0.4f, 0.5f).OnComplete(() =>
        {
            _popupFail.gameObject.SetActive(true);
        });
    }

    public void Disable()
    {
        _panel.enabled = false;
        _panel.DOFade(0f, 0f);
        _popupPlay.gameObject.SetActive(false);
        _popupPause.gameObject.SetActive(false);
        _popupPass.gameObject.SetActive(false);
        _popupFail.gameObject.SetActive(false);
    }
}