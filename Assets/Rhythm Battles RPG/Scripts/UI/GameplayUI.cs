using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;
using UnityEngine.UI;
using static Utils;

public class GameplayUI : MonoBehaviour
{
    [Header("References")]
    public Transform MeasureWindowsParent;
    [Header("Prefabs")]
    public MeasureWindow MeasureWindowPrefab;
    [Header("Settings")]
    public int MaxMeasureWindowsShown;

    private List<MeasureWindow> measureWindows = new List<MeasureWindow>();

    public void CreateMeasureWindow(Measure measure)
    {
        MeasureWindow measureWindow = InstantiateUIElement(MeasureWindowPrefab.gameObject, MeasureWindowsParent, Vector2.zero).GetComponent<MeasureWindow>();
        foreach (Beat tempBeat in measure.Beats)
        {
            measureWindow.CreateBeatUI(tempBeat);
        }
        measureWindows.Add(measureWindow);
    }

    public void RemoveMeasureWindow()
    {
        MeasureWindow firstMeasureWindow = measureWindows[0];
        measureWindows.Remove(firstMeasureWindow);
        Destroy(firstMeasureWindow.gameObject);
        UpdateMeasureWindowsAlphas();
    }

    public void UpdateMeasureWindowsAlphas()
    {
        int measureWindowsCount = measureWindows.Count;
        for (int i = 0; i < measureWindowsCount; i++)
        {
            MeasureWindow tempMeasureWindow = measureWindows[i];
            bool showMeasureWindow = false;
            if (i < MaxMeasureWindowsShown)
            {
                showMeasureWindow = true;
            }
            tempMeasureWindow.ToggleCanvasGroup(showMeasureWindow);
        }
    }

    public void TurnOnBeatUI(KoreographyEvent koreographyEvent)
    {
        MeasureWindow firstMeasureWindow = measureWindows[0];
        firstMeasureWindow.TurnOnBeatUI(koreographyEvent);
    }
}