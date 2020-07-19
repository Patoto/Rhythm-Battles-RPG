using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Manager
{
    public KeyCode HitKey = KeyCode.J;

    public override void ConnectManager()
    {
        GameManager.Instance.InputManager = this;
    }

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
