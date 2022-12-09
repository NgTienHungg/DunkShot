using UnityEngine;

public class BallTail : MonoBehaviour
{
    [SerializeField] private ParticleSystem whiteSmoke;
    [SerializeField] private ParticleSystem blackSmoke, flame;

    public void Renew()
    {
        whiteSmoke.Stop();
        blackSmoke.Stop();
        flame.Stop();
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
        whiteSmoke.Play();
    }

    private void Flaming()
    {
        whiteSmoke.Stop();
        blackSmoke.Play();
        flame.Play();
    }
}