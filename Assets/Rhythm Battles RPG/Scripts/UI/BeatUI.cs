using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatUI : MonoBehaviour
{
    [Header("References")]
    public Image BeatImage;
    public Outline Outline;
    [Header("Settings")]
    public Color OutlineOffColor;
    public Color OutlineOnColor;
    public Color BeatOffColor;
    public Color BeatOnColor;

    public void SetupBeatUI(BeatType beatType)
    {
        SetupBeatColor(beatType);
    }

    private void SetupBeatColor(BeatType beatType)
    {
        switch (beatType)
        {
            case BeatType.None:
                ChangeBeatColor(BeatOffColor);
                break;
            default:
                ChangeBeatColor(BeatOnColor);
                break;
        }
    }

    private void ChangeBeatColor(Color color)
    {
        BeatImage.color = color;
    }
}
