using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class PlayerJumping : PlayerBase
{
    #region Scripts
    
    private PlayerObjectMove _playerObjectMove;

    private PlayerMovement _playerMovement;
    
    #endregion

    #region Variables
    
    #region Serialized Variables
    
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

    [Tooltip("Layer which can be considered ground to Jump on.")]
    [SerializeField] private LayerMask ground;
    #endregion
    
    #region Private variables
    
    private float inputY;

    private bool canJump;
    
    #endregion 
    
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
        }
        else
        {
            canJump = false;
        }
        
        if (rb.velocity.y < 0f)
        {
            fallMultiplier -= Time.deltaTime;
            
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            
            anim.SetFloat("velocityY", rb.velocity.y);
        }
        else if (rb.velocity.y > -0.01f)
        {
            fallMultiplier = mainFallMultiplier;
        }
    }

    private void LateUpdate()
    {
        Debug.Log(GroundCheck());
        anim.SetBool("hasLanded", GroundCheck());
    }

    /// <summary>
    /// Jumps the player with the amount of jumpStrength as added force. 
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if(canJump && !_playerObjectMove.isMoving)
        {
            rb.velocity = new Vector3(0f, jumpStrength, 0f);

            anim.Play("Jumping");
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
