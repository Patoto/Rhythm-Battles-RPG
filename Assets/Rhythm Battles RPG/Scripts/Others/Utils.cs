using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoordinateTypes
{
    x,
    y,
    z
}

public class Utils : MonoBehaviour
{
    public static readonly float Gravity = 1;

    public static float BPMToSeconds(float bpm)
    {
        return 60 / bpm;
    }

    private static Vector3 ModifyVector3Value(Vector3 vector, CoordinateTypes coordinate, float newValue)
    {
        Vector3 modifiedVector3 = vector;
        switch (coordinate)
        {
            case CoordinateTypes.x:
                modifiedVector3 = new Vector3(newValue, vector.y, vector.z);
                break;
            case CoordinateTypes.y:
                modifiedVector3 = new Vector3(vector.x, newValue, vector.z);
                break;
            case CoordinateTypes.z:
                modifiedVector3 = new Vector3(vector.x, vector.y, newValue);
                break;
        }
        return modifiedVector3;
    }

    public static Vector3 ModifyVector3XValue(Vector3 vector, float newValue)
    {
        return ModifyVector3Value(vector, CoordinateTypes.x, newValue);
    }

    public static Vector3 ModifyVector3YValue(Vector3 vector, float newValue)
    {
        return ModifyVector3Value(vector, CoordinateTypes.y, newValue);
    }

    public static Vector3 ModifyVector3ZValue(Vector3 vector, float newValue)
    {
        return ModifyVector3Value(vector, CoordinateTypes.z, newValue);
    }
}
