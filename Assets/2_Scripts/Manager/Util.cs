using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Util
{
    /// <summary>
    /// kiểm tra xem chuột có đang nằm trên UI hay không?
    /// UI có tick RaycastTarget
    /// </summary>
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    /// <summary>
    /// Lấy toạ độ chuột trong đơn vị World (not pixel)
    /// </summary>
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    /// <summary>
    /// Tính góc giữa vector AB với trục Oy
    /// </summary>
    public static float CalculateAngleDeg(Vector3 A, Vector3 B)
    {
        float dx = B.x - A.x;
        float dy = B.y - A.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        return angle;
    }
}