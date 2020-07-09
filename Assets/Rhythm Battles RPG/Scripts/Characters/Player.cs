using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Animator Animator;

    public enum HopDirection
    {
        Left,
        Right
    }

    public void Hop(HopDirection hopDirection)
    {
        switch (hopDirection)
        {
            case HopDirection.Left:
                Animator.Play("HopLeft");
                break;
            case HopDirection.Right:
                Animator.Play("HopRight");
                break;
        }
    }
}