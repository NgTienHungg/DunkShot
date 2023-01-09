using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIVideoSkin : UISkin
{
    [Header("Video")]
    [SerializeField] private Image _progress;
    [SerializeField] private TextMeshProUGUI _remain;

    public override void SetSkin(Skin skin)
    {
        base.SetSkin(skin);

        _progress.fillAmount = 1f * _skin.VideoWatched / _skin.Data.NumberOfVideos;
        _remain.text = (_skin.Data.NumberOfVideos - _skin.VideoWatched).ToString();
    }

    public override void OnClick()
    {
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
        _skin.WatchVideo();
        _remain.text = (_skin.Data.NumberOfVideos - _skin.VideoWatched).ToString();
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