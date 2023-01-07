using TMPro;
using UnityEngine;

public class UIStar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = SaveSystem.GetInt(SaveKey.STAR).ToString();
    }

    private void FixedUpdate()
    {
        text.text = SaveSystem.GetInt(SaveKey.STAR).ToString();
    }
}