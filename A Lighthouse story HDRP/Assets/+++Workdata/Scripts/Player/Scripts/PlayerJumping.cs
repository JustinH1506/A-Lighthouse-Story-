using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumping : PlayerBase
{
    #region Scripts
    
    private PlayerObjectMove _playerObjectMove;

    private PlayerMovement _playerMovement;
    
    #endregion

    #region Varaibles
    
    [Tooltip("How strong the Player jumps.")]
    [SerializeField] private float jumpStrength;

    [Tooltip("Jump strength during jumping.")]
    [SerializeField] private float sprintJumpStrength;

    [SerializeField] private float mainFallMultiplier;
    
    [SerializeField] private float fallMultiplier;
    
    [Tooltip("Time before falling.")]
    [SerializeField] private float coyoteTime;

    [Tooltip("How much the CoyoteTime is.")]
    [SerializeField] private float coyoteTimeCounter;

    [Tooltip("How far the RayCast goes.")]
    [SerializeField] private float rayCastDistance;

    private float inputY;

    private bool canJump;

    [Tooltip("Layer which can be considered ground to Jump on.")]
    [SerializeField] private LayerMask ground;
    
    #endregion
    
    #region Methods

    protected override void Awake()
    {
        base.Awake();

        _playerObjectMove = GetComponent<PlayerObjectMove>();

        _playerMovement = GetComponent<PlayerMovement>();
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
        
        if (rb.velocity.y < 0)
        {
            fallMultiplier -= Time.deltaTime;
            
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > -0.01f)
        {
            fallMultiplier = mainFallMultiplier;
        }
    }

    /// <summary>
    /// Jumps the player with the amount of jumpStrength as added force. 
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if(canJump && !_playerObjectMove.isMoving)
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
