using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIBallSkin : MonoBehaviour
{
    [SerializeField] private Image _ball;
    [SerializeField] private UIPriceTag _priceTag;

    [SerializeField] private GameObject _imageSelected;
    [SerializeField] private GameObject _imageLocked;

    private BallSkin _skin;
    private bool _isSelecting;

    public void Renew()
    {
        _isSelecting = (_skin.Name == SaveSystem.GetString(SaveKey.BALL_SKIN_IN_USE));

        if (_isSelecting)
        {
            _imageSelected.SetActive(true);
            _ball.transform.DOScale(0.96f, 0.45f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            _imageSelected.SetActive(false);
            _ball.transform.DOKill();
            _ball.transform.localScale = Vector3.one;
        }
    }

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
            _priceTag.SetTag(PriceTagType.Normal);
        else
            _priceTag.SetTag(PriceTagType.Medium);

        if (_skin.Unlocked)
        {
            _ball.gameObject.SetActive(true);
            _priceTag.gameObject.SetActive(false);
            _imageLocked.SetActive(false);
        }
        else
        {
            _ball.gameObject.SetActive(false);
            _priceTag.gameObject.SetActive(true);
            _imageLocked.SetActive(true);
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

        if (_isSelecting)
        {
            // exit shop && play game
            UIManager.Instance.CloseCustomize();
            return;
        }

        if (!_skin.Unlocked)
        {
            _skin.Unlock();
            _imageLocked.SetActive(false);
            _ball.gameObject.SetActive(true);
            _priceTag.gameObject.SetActive(false);
        }

        SaveSystem.SetString(SaveKey.BALL_SKIN_IN_USE, _skin.Name);
        Observer.ChangeBallSkin?.Invoke();
        //FindObjectOfType<Ball>().LoadSkin();
        Debug.Log("change ball skin");
    }
}