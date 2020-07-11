using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode HitKey = KeyCode.J;

    public bool PressedHitButton()
    {
        return Input.GetKeyDown(HitKey);
    }

    public bool PressingHitButton()
    {
        return Input.GetKey(HitKey);
    }

    public bool ReleasedHitButton()
    {
        return Input.GetKeyUp(HitKey);
    }
}
