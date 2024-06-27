using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : PlayerBase
{
    #region Scripts
    
    [SerializeField] private PlayerObjectMove _playerObjectMove;
    
    #endregion

    #region Varaibles
    
    [Tooltip("How strong the Player jumps.")]
    [SerializeField] private int jumpStrength;
    
    [Tooltip("Time before falling.")]
    public float coyoteTime;

    [Tooltip("How much the CoyoteTime is.")]
    public float coyoteTimeCounter;

    [SerializeField] private LayerMask ground;

    public bool canJump;
    
    #endregion
    
    #region Methods
    
    /// <summary>
    /// Shoot a raycast down to look for ground. 
    /// </summary>
    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, Vector3.down, .25f, ground))
        {
            canJump = true;

            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTime -= Time.deltaTime;
        }

        if (!Physics.Raycast(transform.position, Vector3.down, .25f, ground) && coyoteTimeCounter <= 0)
        {
            canJump = false;
        }
    }

    /// <summary>
    /// Jumps the player with the amount of jumpStrength as added force. 
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
