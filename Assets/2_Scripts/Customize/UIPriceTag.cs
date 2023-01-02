using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PriceTagType
{
    Normal,
    Medium
}

public class UIPriceTag : MonoBehaviour
{
    [Header("Tag Sprites")]
    [SerializeField] private Sprite _normalTagSprite;
    [SerializeField] private Sprite _mediumTagSprite;

    [Header("Components")]
    [SerializeField] private Image _tag;
    [SerializeField] private TextMeshProUGUI _price;

    public void SetTag(PriceTagType type)
    {
        if (type == PriceTagType.Normal)
        {
            _tag.sprite = _normalTagSprite;
            _price.text = "100";
        }
        else
        {
            _tag.sprite = _mediumTagSprite;
            _price.text = "200";
        }
    }
}