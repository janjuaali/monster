using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class ClickHandler
{
    public static bool IsNotOnUI(ref EventSystem eventSystem, ref GraphicRaycaster canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        List<RaycastResult> results = new List<RaycastResult>();

        pointerEventData.position = Input.mousePosition;
        canvas.Raycast(pointerEventData, results);

        return results.Count == 0;
    }
}
