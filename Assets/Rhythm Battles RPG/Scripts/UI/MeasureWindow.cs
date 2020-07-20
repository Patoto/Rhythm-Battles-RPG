using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class MeasureWindow : MonoBehaviour
{
    [Header("References")]
    public Transform BeatUIParent;
    [Header("Prefabs")]
    public BeatUI BeatUIPrefab;

    public void CreateBeatUI(Beat beat)
    {
        BeatUI beatUI = InstantiateUIElement(BeatUIPrefab.gameObject, BeatUIParent, Vector2.zero).GetComponent<BeatUI>();
    }
}