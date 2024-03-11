using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : PlayerBase
{
    #region Scripts
    
    [SerializeField] private PlayerObjectMove _playerObjectMove;
    
    #endregion

    #region Varaibles
    
    [SerializeField] private int jumpStrength;

    public bool canJump;
    
    #endregion
    
    #region Methods
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump && !_playerObjectMove.isMoving)
           rb.AddForce(new Vector3(rb.velocity.x, jumpStrength, rb.velocity.y), ForceMode.Acceleration);
    }
    
    #endregion
}
