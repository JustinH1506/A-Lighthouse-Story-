using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : PlayerBase
{
    #region Scripts
    
    private PlayerObjectMove _playerObjectMove;
    
    #endregion

    #region Varaibles
    
    [Tooltip("How strong the Player jumps.")]
    [SerializeField] private float jumpStrength;
    
    [Tooltip("Time before falling.")]
    [SerializeField] private float coyoteTime;

    [Tooltip("How much the CoyoteTime is.")]
    [SerializeField] private float coyoteTimeCounter;

    [Tooltip("How far the RayCast goes.")]
    [SerializeField] private float rayCastDistance;

    private bool canJump;

    [Tooltip("Layer which can be considered ground to Jump on.")]
    [SerializeField] private LayerMask ground;
    
    #endregion
    
    #region Methods

    protected override void Awake()
    {
        base.Awake();

        _playerObjectMove = GetComponent<PlayerObjectMove>();
    }

    private void Update()
    {
        if (GroundCheck())
        {
            coyoteTimeCounter = coyoteTime;
            
            canJump = true;
            
            anim.SetTrigger("hasLanded");
        }

        if (!GroundCheck())
        {
            coyoteTimeCounter -= Time.deltaTime;

            if (coyoteTimeCounter >= 0)
            {
                canJump = false;
            }
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
            rb.AddForce(new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z), ForceMode.Impulse);
            
            anim.SetTrigger("isJumping");
        }
    }
    
    /// <summary>
    /// Checks if Player is close enough to the ground to Jump.
    /// </summary>
    /// <returns></returns>
    bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayCastDistance, ground);
    }
    
    #endregion
}
