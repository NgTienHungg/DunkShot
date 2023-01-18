using DG.Tweening;
using UnityEngine;
using System.Collections;

public class PopupFortuneSkin : PopupSkin
{
    [SerializeField] private Transform _leftLight, _rightLight;
    [SerializeField] private Transform _wheel;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SwitchLight());
        StartCoroutine(RotateWheel());
    }

    private IEnumerator SwitchLight()
    {
        while (true)
        {
            _leftLight.localScale = new Vector3(1f, -1 * _leftLight.localScale.y);
            _rightLight.localScale = new Vector3(1f, -1 * _rightLight.localScale.y);
            yield return new WaitForSecondsRealtime(0.4f);
        }
    }

    private IEnumerator RotateWheel()
    {
        while (true)
        {
            _wheel.DORotate(Vector3.forward * -360f * 4, 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad).SetUpdate(true);
            yield return new WaitForSecondsRealtime(3.5f);
        }
    }

    public void OnSpinButton()
    {
        Debug.Log("SPIN");
    }
}