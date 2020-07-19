using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager
{
    public override void ConnectManager()
    {
        GameManager.Instance.UIManager = this;
    }
}