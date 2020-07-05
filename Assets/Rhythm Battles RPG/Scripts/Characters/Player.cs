using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Rigidbody Rigidbody;

    private void Update()
    {
        ApplyGravity();
    }

    public void Jump(float jumpForce)
    {
        float newYVelocity = Rigidbody.velocity.y + jumpForce;
        Rigidbody.velocity = Utils.ModifyVector3YValue(Rigidbody.velocity, newYVelocity);
    }

    private void ApplyGravity()
    {
        float relativeGravity = Utils.Gravity * Time.deltaTime;
        float newYVelocity = Rigidbody.velocity.y - relativeGravity;
        Rigidbody.velocity = Utils.ModifyVector3YValue(Rigidbody.velocity, newYVelocity);
    }
}