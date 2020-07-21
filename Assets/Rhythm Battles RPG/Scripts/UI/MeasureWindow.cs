using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;
using static Utils;

public class MeasureWindow : MonoBehaviour
{
    [Header("References")]
    public Transform BeatUIParent;
    public CanvasGroup CanvasGroup;
    [Header("Prefabs")]
    public BeatUI BeatUIPrefab;

    private List<BeatUI> beatUIs = new List<BeatUI>();

    public void CreateBeatUI(Beat beat)
    {
        BeatUI beatUI = InstantiateUIElement(BeatUIPrefab.gameObject, BeatUIParent, Vector2.zero).GetComponent<BeatUI>();
        beatUI.SetupBeatUI(beat);
        beatUIs.Add(beatUI);
    }

    public void TurnOnBeatUI(KoreographyEvent beatEvent)
    {
        BeatUI beatUI = GetBeatUIWithBeatEvent(beatEvent);
        if (beatUI)
        {
            ToggleBeatUIs(false);
            beatUI.ToggleOutlineColor(true);
        }
    }

    private BeatUI GetBeatUIWithBeatEvent(KoreographyEvent beatEvent)
    {
        BeatUI beatUI = null;
        foreach (BeatUI tempBeatUI in beatUIs)
        {
            if (tempBeatUI.Beat.BeatEvent == beatEvent)
            {
                beatUI = tempBeatUI;
            }
        }
        return beatUI;
    }

    private void ToggleBeatUIs(bool on)
    {
        foreach (BeatUI tempBeatUI in beatUIs)
        {
            tempBeatUI.ToggleOutlineColor(on);
        }
    }

    public void ToggleCanvasGroup(bool on)
    {
        if (on)
        {
            CanvasGroup.alpha = 1;
        }
        else
        {
            CanvasGroup.alpha = 0;
        }
    }
}