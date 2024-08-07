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

    [SerializeField] private CapsuleCollider sneakCollider, nonSneakCollider;
    
    #endregion
    
    #region Variables

    public const string PlayerXKey = "PlayerX";
    public const string PlayerYKey = "PlayerY";
    public const string PlayerZKey = "PlayerZ";
    
    [Header("Movement")]
    [Tooltip("Speed to make the Player Move.")]
    [Space(5)]
    public float acceleration;
    
    [Tooltip("How fast the character loses speed.")]
    public float decelerationSpeed;
    
    [Tooltip("Maximum speed the character can move.")]
    public float maxSpeed;

    [Tooltip("Speed when the Player is in the air.")]
    public float aerialSpeed;
    
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

    [HideInInspector] public bool isDisabled = false;
    
    public float inputX, inputZ;

    private bool isSneaking;
    
    [HideInInspector]
    public bool isSprinting;

    #endregion

    #region Local classes
    [System.Serializable]
    public class Data
    {
        public Transform safePoint;
    }
    
    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        
        maxSpeed = defaultSpeed;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(PlayerXKey))
        {
            anim.Play("Standing Up");
        }
        
        if (PlayerPrefs.HasKey(PlayerXKey))
        {
            Vector3 safePosition = new Vector3(PlayerPrefs.GetFloat(PlayerXKey), PlayerPrefs.GetFloat(PlayerYKey),
                PlayerPrefs.GetFloat(PlayerZKey));
            
            transform.position = safePosition;
        }
    }

    /// <summary>
    /// Calls Movement Method.
    /// </summary>
    public void FixedUpdate()
    {
       Movement();
    }
    
    /// <summary>
    /// Here we get the movement which is based on AddForce.
    /// The movement is based on world space and gets that info from the camera position.
    /// </summary>
    private void Movement()
    {
        if (!LoadSceneManager.instance.sceneLoaded)
        {
            return;
        }
        
        // Disables the movement.
        if (isDisabled) return;
        
        if (_playerObjectMove.isMoving)
        {
            maxSpeed = sneakSpeed;

            isSprinting = false;

            isSneaking = false;
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
                if (rb.velocity.y < -1f)
                {
                    maxSpeed = aerialSpeed;
                }
                
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

        Vector3 animVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        anim.SetFloat("velocity", animVelocity.magnitude);
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
    
    /// <summary>
    /// Slows moveSpeed and start animation. 
    /// </summary>
    /// <param name="context"></param>
    public void Sneak(InputAction.CallbackContext context)
    {
        if (isDisabled) return;
        
        if (context.performed && !isSneaking && !_playerObjectMove.isMoving)
        {
            isSneaking = true;
            
            isSprinting = false;
            
            anim.SetBool("isSneaking", isSneaking);

            sneakCollider.enabled = true;

            nonSneakCollider.enabled = false;
            
            maxSpeed = sneakSpeed;
        }
        else if(!_playerObjectMove.isMoving)
        {
            isSneaking = false;
            
            anim.SetBool("isSneaking", isSneaking);

            sneakCollider.enabled = false;

            nonSneakCollider.enabled = true;
            
            maxSpeed = defaultSpeed;
        }
    }

    /// <summary>
    /// Increases the speed and starts sprint animation. 
    /// </summary>
    /// <param name="context"></param>
    public void Sprint(InputAction.CallbackContext context)
    {
        if (isDisabled) return;
        
        if (context.performed && !isSprinting && !_playerObjectMove.isMoving)
        {
            isSprinting = true;

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
    
    /// <summary>
    /// Method to disable movement and stop Player. 
    /// </summary>
    public void DisableMovement()
    {
        isDisabled = true;
            
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        anim.SetFloat("velocity", 0f);
    }

    public void EnableMovement()
    {
        isDisabled = false;
    }
    #endregion
}