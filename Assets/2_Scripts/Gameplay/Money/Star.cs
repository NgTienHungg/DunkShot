using DG.Tweening;
using UnityEngine;

public class Star : MonoBehaviour
{
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOMoveY(transform.position.y + 0.8f, 1.2f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        });
    }

    public void Disappear()
    {
        transform.DOKill();
        transform.DOScale(0f, 0.4f).OnComplete(() =>
        {
            MoneyManager.Instance.AddStar(1);
            _collider.enabled = true;
            transform.localScale = Vector3.one;
            ObjectPool.Instance.Recall(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AudioManager.Instance.PlaySound(AudioKey.COLLECT_STAR);
            _collider.enabled = false;
            Disappear();
            Observer.CollectStar?.Invoke();
        }
    }
}