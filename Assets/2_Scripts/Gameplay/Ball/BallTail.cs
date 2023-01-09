using UnityEngine;

public class BallTail : MonoBehaviour
{
    [SerializeField] private ParticleSystem _whiteSmoke;
    [SerializeField] private ParticleSystem _blackSmoke;
    [SerializeField] private ParticleSystem _specialTail;

    public void Renew()
    {
        _whiteSmoke.Stop();
        _blackSmoke.Stop();
        _specialTail.Stop();
    }

    private void OnEnable()
    {
        Observer.BallSmoke += Smoking;
        Observer.BallFlame += Flaming;
    }

    private void OnDisable()
    {
        Observer.BallSmoke -= Smoking;
        Observer.BallFlame -= Flaming;
    }

    private void Smoking()
    {
        _whiteSmoke.Play();
    }

    // private 
    public void Flaming()
    {
        _whiteSmoke.Stop();
        _blackSmoke.Play();
        _specialTail.Play();
    }

    public void LoadSkin()
    {
        // load special tail
        _specialTail.textureSheetAnimation.SetSprite(0, DataManager.Instance.SkinInUse.Data.Tail.Sprite);

        var color = _specialTail.colorOverLifetime;
        color.color = DataManager.Instance.SkinInUse.Data.Tail.Color;
    }
}