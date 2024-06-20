using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerBase
{
    #region Classes
    
    [SerializeField] private PlayerObjectMove _playerObjectMove;

    public Data positionData;
    
    #endregion

    #region Components

    [SerializeField] private CapsuleCollider _capsuleCollider;
    
    #endregion
    
    #region Variables
    
    [Header("Movement")]
    [Tooltip("Speed to make the Player Move.")]
    [Space(5)]
    public float acceleration;
    
    [Tooltip("How fast the character loses speed.")]
    public float decelerationSpeed;
    
    [Tooltip("Maximum speed the character can move.")]
    public float maxSpeed;
    
    [Tooltip("Speed the player has during normal walking.")]
    public float defaultSpeed;

    [Tooltip("Speed during sneaking.")]
    public float sneakSpeed;

    [Tooltip("Speed during sprinting.")]
    public float sprintSpeed;
    
    [Header("Rotation")]
    [Tooltip("Speed to make the Player Move.")]
    [Space(5)]
    public float rotationSpeed;

    private float inputX, inputZ;

    private bool isSneaking;
    
    private bool isSprinting;

    public bool isDisabled = false;

    #endregion

    #region Local classes
    [System.Serializable]
    public class Data
    {
        public Transform safePoint;
    }
    
    #endregion
    
    #region Methods

    private void Start()
    {
        var currentPosition = GameStateManager.instance.gameState.positionData;
        
        if (currentPosition != null)
        {
            positionData = currentPosition;
            
            transform.position = positionData.safePoint.position;
        }
    }

    public void FixedUpdate()
    {
       Movement();
    }

    private void Movement()
    {
        if (isDisabled) return;
        
        if (_playerObjectMove.isMoving)
        {
            maxSpeed = 0.25f;
        }
        else if(!isSneaking && !isSprinting)
        {
            maxSpeed = defaultSpeed;
        }
       
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
                /*if (_playerObjectMove.isMoving)
                {
                    _playerObjectMove.moveableObjectRb.AddForce(cameraRelativeMovement * acceleration, ForceMode.Force); 
                }*/
                
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
            
            anim.SetBool("isSneaking", isSneaking);

            _capsuleCollider.height = .5f;

            isSprinting = false;
            
            maxSpeed = sneakSpeed;
        }
        else if(!_playerObjectMove.isMoving)
        {
            isSneaking = false;
            
            anim.SetBool("isSneaking", isSneaking);
            
            _capsuleCollider.height = 1.9f;
            
            maxSpeed = defaultSpeed;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && !isSprinting && !_playerObjectMove.isMoving)
        {
            isSprinting = true;
            
            anim.SetBool("isSprinting", isSprinting);

            isSneaking = false;
            
            anim.SetBool("isSneaking", isSneaking);
            
            maxSpeed = sprintSpeed;
        }
        else if(!_playerObjectMove.isMoving && isSprinting)
        {
            isSprinting = false;

            maxSpeed = defaultSpeed;
        }
    }

    public void DisableMovement()
    {
        isDisabled = true;
            
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    #endregion
}