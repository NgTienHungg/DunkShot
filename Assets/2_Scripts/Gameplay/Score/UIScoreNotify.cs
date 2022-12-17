using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UIScoreNotify : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _uiPerfect;
    [SerializeField] private TextMeshProUGUI _uiBounce;
    [SerializeField] private TextMeshProUGUI _uiScoreAdd;

    private Transform _basket;
    private bool _isShowing;

    private Vector3 startPos1 = new Vector3(0f, 60f);
    private Vector3 startPos2 = new Vector3(0, 100f);
    private Vector3 targetPos1 = new Vector3(0f, 140f);
    private Vector3 targetPos2 = new Vector3(0f, 200f);

    public void Renew()
    {
        _basket = null;
        _isShowing = false;

        SetInfo(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (_isShowing)
        {
            transform.position = _basket.position + Vector3.up * 0.5f;
        }
    }

    public void Show(int comboPerfect, int comboBounce, int scoreAdd)
    {
        _isShowing = true;
        _basket = Controller.Instance.BasketSpawner.CurrentBasket.transform;
        transform.position = _basket.position + Vector3.up * 0.7f;

        SetInfo(comboPerfect, comboBounce, scoreAdd);
        StartCoroutine(ShowDelay(comboPerfect > 0, comboBounce > 0));
    }

    private void SetInfo(int comboPerfect, int comboBounce, int scoreAdded)
    {
        _uiPerfect.DOFade(0f, 0f).SetUpdate(true);
        _uiPerfect.rectTransform.localPosition = startPos1;
        _uiPerfect.text = comboPerfect <= 1 ? "PERFECT!" : string.Format($"PERFECT x{comboPerfect}");

        _uiBounce.DOFade(0f, 0f).SetUpdate(true);
        _uiBounce.rectTransform.localPosition = startPos1;
        _uiBounce.text = comboBounce <= 1 ? "BOUNCE!" : string.Format($"BOUNCE x{comboBounce}");

        _uiScoreAdd.DOFade(0f, 0f).SetUpdate(true);
        _uiScoreAdd.text = string.Format($"+{scoreAdded}");
    }

    private IEnumerator ShowDelay(bool hasPerfect, bool hasBounce)
    {
        if (hasPerfect)
        {
            if (hasBounce)
            {
                _uiPerfect.rectTransform.localPosition = startPos2;
                _uiPerfect.DOFade(0.8f, 0f).OnComplete(() =>
                {
                    _uiPerfect.DOFade(1f, 0.3f);
                    _uiPerfect.rectTransform.DOLocalMove(targetPos2, 1.3f).SetEase(Ease.OutQuad);
                    _uiPerfect.DOFade(0f, 0.3f).SetDelay(1f);
                });
                yield return new WaitForSeconds(0.4f);
            }
            else
            {
                _uiPerfect.DOFade(0.8f, 0f).OnComplete(() =>
                {
                    _uiPerfect.DOFade(1f, 0.3f);
                    _uiPerfect.rectTransform.DOLocalMove(targetPos1, 1.3f).SetEase(Ease.OutQuad);
                    _uiPerfect.DOFade(0f, 0.3f).SetDelay(1f);
                });
                yield return new WaitForSeconds(0.4f);
            }
        }

        if (hasBounce)
        {
            _uiBounce.DOFade(0.8f, 0f).OnComplete(() =>
            {
                _uiBounce.rectTransform.localPosition = startPos1;
                _uiBounce.rectTransform.DOLocalMove(targetPos1, 1.3f).SetEase(Ease.OutQuad);
                _uiBounce.DOFade(0f, 0.3f).SetDelay(1f);
            });
            yield return new WaitForSeconds(0.4f);
        }

        _uiScoreAdd.DOFade(0.8f, 0f).OnComplete(() =>
        {
            _uiScoreAdd.DOFade(1f, 0.3f);
            _uiScoreAdd.rectTransform.localPosition = Vector3.zero;
            _uiScoreAdd.rectTransform.DOLocalMoveY(60f, 1.3f).SetEase(Ease.OutQuad);
            _uiScoreAdd.DOFade(0f, 0.3f).SetDelay(1f);
        });
    }
}