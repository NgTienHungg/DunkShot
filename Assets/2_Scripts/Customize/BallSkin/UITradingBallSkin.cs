using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UITradingBallSkin : UIBallSkin
{
    [Header("Trading")]
    [SerializeField] private Image _tag;
    [SerializeField] private TextMeshProUGUI _price;

    [Header("Tag Sprites")]
    [SerializeField] private Sprite _normalTagSprite;
    [SerializeField] private Sprite _mediumTagSprite;

    public override void SetSkin(BallSkin skin)
    {
        base.SetSkin(skin);

        if (_skin.Unlocked)
        {
            _ball.gameObject.SetActive(true);
            _locked.gameObject.SetActive(false);
            _tag.gameObject.SetActive(false);
        }
        else
        {
            _ball.gameObject.SetActive(false);
            _locked.gameObject.SetActive(true);
            _tag.gameObject.SetActive(true);
        }

        if (_skin.Data.Price == 100)
            _tag.sprite = _normalTagSprite;
        else
            _tag.sprite = _mediumTagSprite;

        _price.text = _skin.Data.Price.ToString();
    }

    public void OnClick()
    {
        // audio

        if (_isSelecting)
        {
            UIManager.Instance.CloseCustomize();
            return;
        }

        if (!_skin.Unlocked)
        {
            if (SaveSystem.GetInt(SaveKey.STAR) < _skin.Data.Price)
            {
                Debug.Log("NOT ENOUGH");
                return;
            }

            Debug.Log("BUY SKIN");
            Unlock();
        }

        Select();
    }

    protected override void Unlock()
    {
        base.Unlock();

        _tag.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            _tag.gameObject.SetActive(false);
        });
    }
}