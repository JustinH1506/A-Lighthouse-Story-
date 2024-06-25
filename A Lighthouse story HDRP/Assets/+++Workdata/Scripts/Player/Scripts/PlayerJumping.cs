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
    
    /// <summary>
    /// canJump will be true if other is ground layer. 
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != ground)
        {
            canJump = true; 
        }
    }

    /// <summary>
    /// canJump will be true if other is ground layer.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == ground)
        {
            canJump = true; 
        }
    }

    /// <summary>
    /// Jumps the player with the amount of jumpSrength as added force. 
    /// </summary>
    /// <param name="context"></param>
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
