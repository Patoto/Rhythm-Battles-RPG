using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static float BPMToSeconds(float bpm)
    {
        return 60 / bpm;
    }
}
