using UnityEngine;
using UnityEngine.UI;

public class UIBallSkin : MonoBehaviour
{
    [SerializeField] private Image _ball;
    [SerializeField] private UIPriceTag _priceTag;

    [SerializeField] private GameObject _selected;
    [SerializeField] private GameObject _lock;

    private BallSkin _skin;

    public void SetSkin(BallSkin skin)
    {
        _skin = skin;
        _ball.sprite = _skin.Data.Sprite;

        switch (_skin.Data.Type)
        {
            case SkinType.TradingBall:
                SetUITradingBallSkin();
                break;
            case SkinType.VideoBall:
                SetUIVideoBallSkin();
                break;
            case SkinType.MissionBall:
                SetUIMissionBallSkin();
                break;
            case SkinType.ChallengeBall:
                SetUIChallengeBallSkin();
                break;
            case SkinType.SecretBall:
                SetUISecretBallSkin();
                break;
            case SkinType.FortuneBall:
                SetUIFortuneBallSkin();
                break;
        }
    }

    private void SetUITradingBallSkin()
    {
        if (_skin.Data.Price == 100)
        {
            _priceTag.SetTag(PriceTagType.Normal);
        }
        else
        {
            _priceTag.SetTag(PriceTagType.Medium);
        }

        if (_skin.Unlocked)
        {
            _ball.gameObject.SetActive(true);
            _priceTag.gameObject.SetActive(false);
            _lock.SetActive(false);
        }
        else
        {
            _ball.gameObject.SetActive(false);
            _priceTag.gameObject.SetActive(true);
            _lock.SetActive(true);
        }
    }

    private void SetUIVideoBallSkin()
    {

    }

    private void SetUIMissionBallSkin()
    {

    }

    private void SetUIChallengeBallSkin()
    {

    }

    private void SetUISecretBallSkin()
    {

    }

    private void SetUIFortuneBallSkin()
    {

    }

    public void OnClick()
    {
        // audio
        if (_skin.Unlocked)
        {

        }
    }
}