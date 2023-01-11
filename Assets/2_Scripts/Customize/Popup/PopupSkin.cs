using DG.Tweening;
using UnityEngine;

public class PopupSkin : MonoBehaviour
{
    protected Transform _popup;
    protected Skin _skin;

    protected virtual void Awake()
    {
        _popup = transform.GetChild(0);
    }

    protected virtual void OnEnable()
    {
        _popup.localScale = Vector3.zero;
        _popup.DOScale(1f, 0.4f).SetEase(Ease.OutExpo).SetUpdate(true);
    }

    public virtual void Show(Skin skin)
    {
        _skin = skin;
    }

    public virtual void OnCancelButton()
    {
        gameObject.SetActive(false);
    }
}