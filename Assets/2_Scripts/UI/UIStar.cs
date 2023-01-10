using TMPro;
using UnityEngine;

public class UIStar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = MoneyManager.Instance.Star.ToString();
    }

    private void FixedUpdate()
    {
        text.text = MoneyManager.Instance.Star.ToString();
    }
}