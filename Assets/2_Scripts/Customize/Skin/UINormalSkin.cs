using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UINormalSkin : UISkin
{
    [Header("Normal")]
    [SerializeField] private Image _tagImage;
    [SerializeField] private TextMeshProUGUI _price;

    [Header("Tag Sprites")]
    [SerializeField] private Sprite _normalTagSprite;
    [SerializeField] private Sprite _mediumTagSprite;

    public override void SetSkin(Skin skin)
    {
        base.SetSkin(skin);

        if (_skin.Data.Price == 100)
            _tagImage.sprite = _normalTagSprite;
        else
            _tagImage.sprite = _mediumTagSprite;

        _price.text = _skin.Data.Price.ToString();
    }

    public override void OnClick()
    {
        if (_isSelecting)
        {
            CanvasController.Instance.CloseCustomize();
            return;
        }

        if (!_skin.Unlocked)
        {
            if (!MoneyManager.Instance.CanBuyByStar(_skin.Data.Price))
            {
                Debug.LogWarning("NOT ENOUGH");
                Observer.OnShowSkinPopup?.Invoke(_skin);
                return;
            }
            Unlock();
        }
        Select();
    }

    protected override void Unlock()
    {
        MoneyManager.Instance.BuyByStar(_skin.Data.Price);

        base.Unlock();

        _tagImage.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            _tagImage.gameObject.SetActive(false);
        });
    }
}