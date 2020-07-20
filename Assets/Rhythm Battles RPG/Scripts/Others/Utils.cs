using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeatType
{
    None,
    A,
    B,
    C,
    D
}

public class Utils : MonoBehaviour
{
    public static GameObject InstantiateUIElement(GameObject prefab, Transform parent, Vector2 localPosition)
    {
        GameObject uiElement = Instantiate(prefab);
        uiElement.transform.SetParent(parent, false);
        uiElement.transform.localPosition = localPosition;
        return uiElement;
    }
}

public static class TrackNames
{
    public static string Measure = "Measure";
    public static string Beat = "Beat";
}