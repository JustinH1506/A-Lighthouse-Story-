using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerBase
{
    #region Scripts
    
    [SerializeField] private PlayerObjectMove _playerObjectMove;
    
    #endregion 
    
    #region Variables
    
    [Header("Movement")]
    [Tooltip("Speed to make the Player Move (Has to be negative.)")]
    [Space(5)]
    public float acceleration;
    
    [Tooltip("How fast the character loses speed.")]
    public float decelerationSpeed;
    
    [Tooltip("Maximum speed the character can move.")]
    public float maxSpeed;
    
    [Header("Rotation")]
    [Tooltip("Speed to make the Player Move (Has to be negative.)")]
    [Space(5)]
    public float rotationSpeed;

    [SerializeField] private float fallSpeed;

    private float inputX, inputZ;

    private bool isSneaking;
    
    private bool isSprinting;

    #endregion
    
    #region Methods
    
    public void FixedUpdate()
    {
       Movement();
    }

    private void Movement()
    {
        if(inputX != 0 || inputZ != 0)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward = cameraForward.normalized;
            cameraRight = cameraRight.normalized;

            Vector3 forwardRelativeMovementVector = inputZ * cameraForward;
            Vector3 rightRelativeMovementVector = inputX * cameraRight;

            Vector3 cameraRelativeMovement = forwardRelativeMovementVector + rightRelativeMovementVector;
            cameraRelativeMovement.Normalize();

            if (cameraRelativeMovement != Vector3.zero)
            {
                rb.AddForce(cameraRelativeMovement * acceleration, ForceMode.Force);

                if (rb.velocity.magnitude > maxSpeed)
                {
                    float yAxis = rb.velocity.y;
                    
                    rb.velocity = rb.velocity.normalized * maxSpeed;

                    rb.velocity = new Vector3(rb.velocity.x, yAxis, rb.velocity.z);
                }
            }
            else
            {
                rb.AddForce(rb.velocity * -decelerationSpeed, ForceMode.Force);
            }

            if (cameraRelativeMovement != Vector3.zero && !_playerObjectMove.isMoving)
            {
                Quaternion toRotation = Quaternion.LookRotation(cameraRelativeMovement, Vector3.up);

                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    
    /// <summary>
    /// We save the x and z value in inputX and inputZ.
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector3>().x;
        
        inputZ = context.ReadValue<Vector3>().z;
    }
    
    public void Sneak(InputAction.CallbackContext context)
    {
        if (context.performed && !isSneaking && !_playerObjectMove.isMoving)
        {
            isSneaking = true;

            isSprinting = false;
            
            maxSpeed = 0.5f;
        }
        else if(!_playerObjectMove.isMoving)
        {
            isSneaking = false;

            maxSpeed = 1;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && !isSprinting && !_playerObjectMove.isMoving)
        {
            isSprinting = true;

            isSneaking = false;
            
            maxSpeed = 1.5f;
        }
        else if(!_playerObjectMove.isMoving)
        {
            isSprinting = false;

            maxSpeed = 1;
        }
    }
    #endregion
}