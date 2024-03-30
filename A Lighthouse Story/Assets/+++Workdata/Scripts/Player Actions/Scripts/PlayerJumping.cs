using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : PlayerBase
{
    #region Scripts
    
    [SerializeField] private PlayerObjectMove _playerObjectMove;
    
    #endregion

    #region Varaibles
    
    [SerializeField] private int jumpStrength;

    [SerializeField] private LayerMask ground;

    public bool canJump;
    
    #endregion
    
    #region Methods

    private void OnCollisionEnter(Collision other)
    {
        canJump = true;
    }

    private void OnCollisionStay(Collision other)
    {
        canJump = true;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump && !_playerObjectMove.isMoving)
        {
            rb.AddForce(new Vector3(rb.velocity.x, jumpStrength, rb.velocity.y), ForceMode.Acceleration);
            canJump = false;
        }
    }
    
    #endregion
}
