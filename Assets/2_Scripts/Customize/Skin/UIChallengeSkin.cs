using DG.Tweening;

public class UIChallengeSkin : UISkin
{
    public override void Renew()
    {
        base.Renew();

        if (_skin.Unlocked && _locked.gameObject.activeInHierarchy)
        {
            _ball.gameObject.SetActive(true);
            _locked.DOFade(0f, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                _locked.gameObject.SetActive(false);
            });
        }
    }
}