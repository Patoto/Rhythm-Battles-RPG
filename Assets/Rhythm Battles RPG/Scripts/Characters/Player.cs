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

    private HopDirection lastHopDirection = HopDirection.Right;
    private HitDirection lastHitDirection = HitDirection.Right;

    public void Hop()
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

    private HopDirection GetNewHopDirection()
    {
        HopDirection newHopDirection = HopDirection.Left;
        if (lastHopDirection == HopDirection.Left)
        {
            newHopDirection = HopDirection.Right;
        }
        return newHopDirection;
    }

    public void Hit()
    {
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