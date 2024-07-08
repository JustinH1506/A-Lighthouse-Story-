using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerObjectMove : PlayerBase
{
    #region Classes

    private PlayerMovement playerMovement;
    
    #endregion
    
    #region Variables
    [Tooltip("Maximum distance for the Raycast")]
    [Range(0, 10)]
    [SerializeField] private float raycastDistance;

    [SerializeField] private float pushPower;

    [Tooltip("Offsets the Raycast.")]
    [SerializeField] private float offset;
    
    private float inputX, inputZ;
    
    public bool isMoving;

    private bool isBranch;
    
    [SerializeField] private Transform moveableObjectParent;

    [SerializeField] private LayerMask targetLayer;
    #endregion

    #region Components

    private SpringJoint _springJoint;

    private FixedJoint _fixedJoint;

    [SerializeField] private PhysicMaterial zeroFriction;

    #endregion
    
    #region Objects
    
    private GameObject moveableObject;

    public Rigidbody moveableObjectRb;
    
    #endregion

    #region Methods

    /// <summary>
    /// Get SpringJoint in Children.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        
        _springJoint = GetComponentInChildren<SpringJoint>();

        playerMovement = GetComponent<PlayerMovement>();
        
        
    }

    /// <summary>
    /// Shoots a Raycast to find moveable objects and sets one if found to moveableObject. 
    /// </summary>
    private void FixedUpdate()
    {
        RaycastHit hit;
        
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
        
        Debug.DrawRay(raycastOrigin, transform.forward, Color.green);
        
        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, raycastDistance,targetLayer))
        {
            if(hit.collider.CompareTag("Moveable"))
            {
                moveableObject = hit.collider.gameObject;

                isBranch = false;
            }
            else if (hit.collider.CompareTag("Branch"))
            {
                moveableObject = hit.collider.gameObject;
                
                isBranch = true;
            }
        }
        else
        {
            moveableObject = null;

             isMoving = false;
        }

        if(isMoving && !isBranch)
        {
            if(Vector2.Angle(new Vector2(rb.velocity.x, rb.velocity.z), new Vector2(transform.forward.x,transform.forward.z)) > 75f)
            {
                anim.SetBool("isPushing", false);
                anim.SetBool("isPulling", true);
            }
            
            if(Vector2.Angle(new Vector2(rb.velocity.x, rb.velocity.z), new Vector2(transform.forward.x,transform.forward.z)) < 75f)
            {
                anim.SetBool("isPushing", true);
                anim.SetBool("isPulling", false);
            }
        }
    }

    /// <summary>
    /// We connect the Rigidbody to our SpringJoint to have it moving. 
    /// </summary>
    /// <param name="context"></param>
    public void ConnectObject(InputAction.CallbackContext context)
    {
        if (moveableObject != null && !isBranch)
        {
            moveableObject.GetComponent<BoxCollider>().material = zeroFriction;

            _fixedJoint = moveableObject.gameObject.AddComponent<FixedJoint>();

            _fixedJoint.connectedBody = rb;
            
            isMoving = true;
        }
    }
    
    /// <summary>
    /// We disconnect the spring joint to make it not moveable. 
    /// </summary>
    /// <param name="context"></param>
    public void DisconnectObject(InputAction.CallbackContext context)
    {
        if(moveableObject != null && !isBranch)
        {
            moveableObjectRb = null;

            _fixedJoint.connectedBody = null;
            
            Destroy(moveableObject.gameObject.GetComponent<FixedJoint>());

            moveableObject.GetComponent<BoxCollider>().material = null;
            
            isMoving = false;
            
            anim.SetBool("isPushing", false);
            anim.SetBool("isPulling", false);
        }
    }

    /// <summary>
    /// Connects the branch to the player. 
    /// </summary>
    /// <param name="context"></param>
    public void ConnectBranch(InputAction.CallbackContext context)
    {
        if (moveableObject != null && isBranch)
        {
            moveableObjectRb = moveableObject.GetComponent<Rigidbody>();

            moveableObjectRb.mass = 1f;
            
            isMoving = true;
        }
    }
    
    /// <summary>
    /// Disconnects the branch from the player. 
    /// </summary>
    /// <param name="context"></param>
    public void DisconnectBranch(InputAction.CallbackContext context)
    {
        if (moveableObject != null && isBranch)
        {
            moveableObjectRb.mass = 100f;
            
            moveableObjectRb = null;
            
            isMoving = false;
        }
    }

    #endregion
}