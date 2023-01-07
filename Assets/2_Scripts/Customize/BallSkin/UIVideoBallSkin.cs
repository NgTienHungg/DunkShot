using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIVideoBallSkin : UIBallSkin
{
    [Header("Video")]
    [SerializeField] private GameObject _tag;
    [SerializeField] private Image _progress;
    [SerializeField] private TextMeshProUGUI _remain;

    public override void SetSkin(BallSkin skin)
    {
        base.SetSkin(skin);

        if (_skin.Unlocked)
        {
            _ball.gameObject.SetActive(true);
            _locked.gameObject.SetActive(false);
            _tag.SetActive(false);
        }
        else
        {
            _ball.gameObject.SetActive(false);
            _locked.gameObject.SetActive(true);
            _tag.SetActive(true);
        }

        // set tag
        _progress.fillAmount = 1f * _skin.VideoWatched / _skin.Data.NumberOfVideos;
        _remain.text = (_skin.Data.NumberOfVideos - _skin.VideoWatched).ToString();
    }

    public void OnClick()
    {
        // audio

        if (_isSelecting)
        {
            UIManager.Instance.CloseCustomize();
            return;
        }

        if (_skin.VideoWatched < _skin.Data.NumberOfVideos)
            WatchVideo();
        else
            Select();
    }

    private void WatchVideo()
    {
        Debug.Log("WATCH VIDEO");

        _skin.WatchVideo();

        _progress.DOFillAmount(1f * _skin.VideoWatched / _skin.Data.NumberOfVideos, 0.8f).SetEase(Ease.OutSine).SetUpdate(true);

        if (_skin.VideoWatched == _skin.Data.NumberOfVideos)
        {
            Unlock();
        }
    }

    protected override void Unlock()
    {
        base.Unlock();

        _tag.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            _tag.SetActive(false);
        });

        Select();
    }
}