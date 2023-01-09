using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISkin : MonoBehaviour
{
    [SerializeField] protected Image _ball;
    [SerializeField] protected Image _selected;
    [SerializeField] protected Image _locked;
    [SerializeField] protected GameObject _tag;

    protected Skin _skin;
    protected bool _isSelecting;

    /// <summary>
    /// default: lock skin
    /// </summary>
    protected virtual void Awake()
    {
        _ball.gameObject.SetActive(false);
        _selected.gameObject.SetActive(false);
        _locked.gameObject.SetActive(true);
        _tag.SetActive(true);
    }

    public virtual void Renew()
    {
        _isSelecting = (_skin.Name == SaveSystem.GetString(SaveKey.BALL_SKIN_IN_USE));

        if (_isSelecting)
        {
            _selected.gameObject.SetActive(true);
            _selected.transform.DOScale(0.8f, 0f).SetUpdate(true).OnComplete(() =>
            {
                _selected.transform.DOScale(1f, 0.3f).SetEase(Ease.OutCubic).SetUpdate(true);
            });
            _ball.transform.DOScale(0.94f, 0.45f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        }
        else
        {
            _selected.gameObject.SetActive(false);
            _ball.transform.DOKill();
            _ball.transform.localScale = Vector3.one;
        }
    }

    public virtual void SetSkin(Skin skin)
    {
        _skin = skin;
        _ball.sprite = _skin.Data.Sprite;

        if (_skin.Unlocked)
        {
            _ball.gameObject.SetActive(true);
            _locked.gameObject.SetActive(false);
            _tag.SetActive(false);
        }
    }

    protected virtual void Select()
    {
        SaveSystem.SetString(SaveKey.BALL_SKIN_IN_USE, _skin.Name);
        Observer.ChangeSkin?.Invoke();
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

    public virtual void OnClick()
    {
        if (!_skin.Unlocked)
        {
            Observer.ShowPopup?.Invoke(_skin);
            return;
        }

        if (_isSelecting)
        {
            UIManager.Instance.CloseCustomize();
        }
        else
        {
            Select();
        }
    }
}