using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [HideInInspector] public delegate void State();
    [HideInInspector] public State CurrentState = null;

    protected int stateCounter = 0;

    public void ChangeState(State state)
    {
        CurrentState = state;
        stateCounter = 0;
        if (IsInvoking("UpdateStateCounter"))
        {
            CancelInvoke("UpdateStateCounter");
        }
    }

    protected void StateWait(float seconds)
    {
        if (!IsInvoking("UpdateStateCounter"))
        {
            Invoke("UpdateStateCounter", seconds);
        }
    }

    private void UpdateStateCounter()
    {
        stateCounter++;
    }
}
