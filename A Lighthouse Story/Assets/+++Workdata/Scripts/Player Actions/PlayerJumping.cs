using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : PlayerBase
{
    [SerializeField] private int jumpStrength;

    public bool canJump;

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && canJump)
           rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
    }
}
