using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum HopDirection
    {
        Left,
        Right
    }

    private enum HitDirection
    {
        Left,
        Right
    }

    [Header("References")]
    public Animator Animator;
    [Header("Settings")]
    public float HitTime;

    private HopDirection lastHopDirection = HopDirection.Right;
    private HitDirection lastHitDirection = HitDirection.Right;
    private bool hitting = false;

    private void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (GameManager.Instance.InputManager.PressedHitButton())
        {
            Hit();
        }
    }

    public void Hop()
    {
        if (!hitting)
        {
            HopDirection hopDirection = GetNewHopDirection();
            switch (hopDirection)
            {
                case HopDirection.Left:
                    Animator.Play("HopLeft");
                    break;
                case HopDirection.Right:
                    Animator.Play("HopRight");
                    break;
            }
            lastHopDirection = hopDirection;
        }
    }

    private HopDirection GetNewHopDirection()
    {
        HopDirection newHopDirection = HopDirection.Left;
        if (lastHopDirection == HopDirection.Left)
        {
            newHopDirection = HopDirection.Right;
        }
        return newHopDirection;
    }

    private void Hit()
    {
        hitting = true;
        HitDirection hitDirection = GetNewHitDirection();
        switch (hitDirection)
        {
            case HitDirection.Left:
                Animator.Play("HitLeft");
                break;
            case HitDirection.Right:
                Animator.Play("HitRight");
                break;
        }
        lastHitDirection = hitDirection;
        CancelInvoke("OnFinishedHit");
        Invoke("OnFinishedHit", HitTime);
    }

    private void OnFinishedHit()
    {
        hitting = false;
    }

    private HitDirection GetNewHitDirection()
    {
        HitDirection newHitDirection = HitDirection.Left;
        if (lastHitDirection == HitDirection.Left)
        {
            newHitDirection = HitDirection.Right;
        }
        return newHitDirection;
    }
}