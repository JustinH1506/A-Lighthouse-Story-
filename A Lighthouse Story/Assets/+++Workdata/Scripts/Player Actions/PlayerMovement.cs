using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerBase
{
    #region Variables
    
    [Header("Movement")]
    [Tooltip("Speed to make the Player Move (Has to be negative.)")]
    [Space(5)]
    [SerializeField] private float moveSpeed, rotationSpeed;

    private float inputX, inputZ;

    private bool isSneaking;
    
    private bool isSprinting;

    #endregion
    
    #region Methods
    public void FixedUpdate()
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

        transform.Translate(cameraRelativeMovement * moveSpeed * Time.deltaTime, Space.World);
        
        if (cameraRelativeMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(cameraRelativeMovement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector3>().x;
        
        inputZ = context.ReadValue<Vector3>().z;
    }

    public void Sneak(InputAction.CallbackContext context)
    {
        if (context.performed && !isSneaking)
        {
            isSneaking = true;

            isSprinting = false;
            
            moveSpeed = 3;
        }
        else
        {
            isSneaking = false;

            moveSpeed = 7;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && !isSprinting)
        {
            isSprinting = true;

            isSneaking = false;
            
            moveSpeed = 9;
        }
        else
        {
            isSprinting = false;

            moveSpeed = 7;
        }
            
    }
    #endregion
}