using UnityEngine;

public class BasketHoop : MonoBehaviour
{
    [Header("Active")]
    [SerializeField] private SpriteRenderer frontHoopActive, backHoopActive;

    [Header("Inactive")]
    [SerializeField] private SpriteRenderer frontHoopInactive, backHoopInactive;
}