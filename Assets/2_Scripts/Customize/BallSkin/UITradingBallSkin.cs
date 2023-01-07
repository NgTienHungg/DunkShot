using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UITradingBallSkin : UIBallSkin
{
    [Header("Trading")]
    [SerializeField] private Image _tagImage;
    [SerializeField] private TextMeshProUGUI _price;

    [Header("Tag Sprites")]
    [SerializeField] private Sprite _normalTagSprite;
    [SerializeField] private Sprite _mediumTagSprite;

    public override void SetSkin(BallSkin skin)
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
        base.OnClick();

        if (_isSelecting) return;

        if (!_skin.Unlocked)
        {
            if (SaveSystem.GetInt(SaveKey.STAR) < _skin.Data.Price)
            {
                Debug.LogWarning("NOT ENOUGH");
                return;
            }

            Buy();
            Unlock();
        }

        Select();
    }

    private void Buy()
    {
        SaveSystem.SetInt(SaveKey.STAR, SaveSystem.GetInt(SaveKey.STAR) - _skin.Data.Price);
    }

    protected override void Unlock()
    {
        base.Unlock();

        _tagImage.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            _tagImage.gameObject.SetActive(false);
        });
    }
}