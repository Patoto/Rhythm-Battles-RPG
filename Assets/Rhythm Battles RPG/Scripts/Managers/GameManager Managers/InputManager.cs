using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Manager
{
    public override void ConnectManager()
    {
        GameManager.Instance.InputManager = this;
    }

    public bool PressedHitButton()
    {
        return Input.anyKeyDown;
    }
}
