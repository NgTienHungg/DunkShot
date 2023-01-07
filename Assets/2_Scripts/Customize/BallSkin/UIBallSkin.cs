using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIBallSkin : MonoBehaviour
{
    [SerializeField] protected Image _ball;
    [SerializeField] protected Image _selected;
    [SerializeField] protected Image _locked;

    protected BallSkin _skin;
    protected bool _isSelecting;

    public virtual void Renew()
    {
        _isSelecting = (_skin.Name == SaveSystem.GetString(SaveKey.BALL_SKIN_IN_USE));

        if (_isSelecting)
        {
            _selected.gameObject.SetActive(true);
            _ball.transform.DOScale(0.96f, 0.45f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        }
        else
        {
            _selected.gameObject.SetActive(false);
            _ball.transform.DOKill();
            _ball.transform.localScale = Vector3.one;
        }
    }

    public virtual void SetSkin(BallSkin skin)
    {
        _skin = skin;
        _ball.sprite = _skin.Data.Sprite;
    }

    protected virtual void Select()
    {
        SaveSystem.SetString(SaveKey.BALL_SKIN_IN_USE, _skin.Name);
        Observer.ChangeBallSkin?.Invoke();
    }

    protected virtual void Unlock()
    {
        _skin.Unlock();

        _ball.gameObject.SetActive(true);

        _locked.DOFade(0f, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            _locked.gameObject.SetActive(false);
        });
    }
}