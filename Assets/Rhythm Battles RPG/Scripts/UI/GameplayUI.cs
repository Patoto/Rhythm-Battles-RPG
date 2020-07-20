using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class GameplayUI : MonoBehaviour
{
    [Header("References")]
    public Transform MeasureWindowsParent;
    [Header("Prefabs")]
    public MeasureWindow MeasureWindowPrefab;

    public void CreateMeasureWindow(Measure measure)
    {
        MeasureWindow measureWindow = InstantiateUIElement(MeasureWindowPrefab.gameObject, MeasureWindowsParent, Vector2.zero).GetComponent<MeasureWindow>();
        foreach (Beat tempBeat in measure.Beats)
        {
            measureWindow.CreateBeatUI(tempBeat);
        }
    }
}