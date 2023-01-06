using DG.Tweening;
using UnityEngine;

public class BasketTrajectory : MonoBehaviour
{
    [SerializeField] private GameObject _dotPrefab;
    [SerializeField] private Transform _dotParent;
    [SerializeField] private int _dotCount;
    [SerializeField] private float _dotDistance;

    private Transform[] _dots;

    public void Renew()
    {
        transform.localScale = Vector3.one;
    }

    private void Awake()
    {
        PrepareDots();
    }

    private void PrepareDots()
    {
        _dots = new Transform[_dotCount];

        for (int i = 0; i < _dotCount; i++)
        {
            _dots[i] = Instantiate(_dotPrefab, _dotParent).transform;
        }

        float x = -((float)_dotCount / 2 - 0.5f);

        for (int i = 0; i < _dotCount; i++)
        {
            _dots[i].localPosition = x * _dotDistance * Vector3.right;
            x++;
        }
    }

    public void Appear()
    {
        foreach (var dot in _dots)
        {
            dot.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
            dot.GetComponent<SpriteRenderer>().DOFade(1f, 0.4f);
        }
    }

    public void Disappear()
    {
        foreach (var dot in _dots)
        {
            dot.GetComponent<SpriteRenderer>().DOFade(0f, 0.3f);
        }

        transform.DOScale(1f, 0.3f).OnComplete(() =>
        {
            ObjectPool.Instance.Recall(gameObject);
        });
    }

    public float Length
    {
        get
        {
            return _dotDistance * (_dotCount - 1);
        }
    }
}