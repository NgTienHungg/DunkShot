using DG.Tweening;
using UnityEngine;

public enum ObstacleType
{
    Bar1,
    Bar2,
    Bar3,
    Bar4,
    Shield1,
    Shield2,
    Shield3,
    Shield4,
    Backboard
}

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType _type;

    private SpriteRenderer _renderer;
    private Quaternion _startRotation;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _startRotation = transform.rotation;

        LoadTheme();

        Observer.ChangeTheme += LoadTheme;
    }

    private void LoadTheme()
    {
        switch (_type)
        {
            case ObstacleType.Bar1:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Bars[0];
                break;
            case ObstacleType.Bar2:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Bars[1];
                break;
            case ObstacleType.Bar3:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Bars[2];
                break;
            case ObstacleType.Bar4:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Bars[3];
                break;
            case ObstacleType.Shield1:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Shields[0];
                break;
            case ObstacleType.Shield2:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Shields[1];
                break;
            case ObstacleType.Shield3:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Shields[2];
                break;
            case ObstacleType.Shield4:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Shields[3];
                break;
            case ObstacleType.Backboard:
                _renderer.sprite = DataManager.Instance.ThemeInUse.Data.Obstacle.Backboard;
                break;
        }
    }

    public void Renew()
    {
        transform.localScale = Vector3.one;
        transform.rotation = _startRotation;
        transform.DOKill(); // kill anim rotate
    }

    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutExpo).SetDelay(0.1f);
    }

    public void Disappear()
    {
        transform.DOKill(); // kill anim rotate
        transform.DOScale(0f, 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            ObjectPool.Instance.Recall(gameObject);
        });
    }
}