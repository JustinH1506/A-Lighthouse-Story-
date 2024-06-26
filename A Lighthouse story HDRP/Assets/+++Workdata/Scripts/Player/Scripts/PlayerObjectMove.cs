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

    [SerializeField] private Transform moveableObjectParent;

    private float inputX, inputZ;

    [HideInInspector]
    public bool isMoving;

    [Tooltip("Offsets the Raycast.")]
    [SerializeField] private float offset;

    [SerializeField] private LayerMask targetLayer;
    #endregion

    #region Components

    private SpringJoint _springJoint;

    #endregion
    
    #region Objects
    
    private GameObject moveableObject;

    public Rigidbody moveableObjectRb;
    
    #endregion

    #region Methods

    /// <summary>
    /// Get SpringJoint in Children.
    /// </summary>
    private void Awake()
    {
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
                Debug.Log("Chest got it");
                moveableObject = hit.collider.gameObject;
            }
        }
        else
        {
            moveableObject = null;
        }

        if(isMoving && moveableObject != null && moveableObjectRb != null)
        {
            Debug.Log("Lol");
            moveableObjectRb.velocity = rb.velocity;
            
            transform.LookAt(new Vector3(moveableObject.transform.position.x, transform.position.y, moveableObject.transform.position.z));
        }
    }

    /// <summary>
    /// We connect the Rigidbody to our SpringJoint ti have it moving. 
    /// </summary>
    /// <param name="context"></param>
    public void ConnectObject(InputAction.CallbackContext context)
    {
        if (moveableObject != null)
        {
            moveableObjectRb = moveableObject.GetComponent<Rigidbody>();

            //_springJoint.connectedBody = moveableObjectRb;

            //moveableObjectRb.mass = 1;
            
            //moveableObject.transform.SetParent(transform);
            
            isMoving = true;
        }
    }
    
    /// <summary>
    /// We disconnect the spring joint to make it not moveable. 
    /// </summary>
    /// <param name="context"></param>
    public void DisconnectObject(InputAction.CallbackContext context)
    {
        if(moveableObject != null)
        {
            //moveableObject.transform.SetParent(moveableObjectParent);
            
            isMoving = false;
            
            //_springJoint.connectedBody = null;
            
            //moveableObjectRb.mass = 100;
        }
    }
    #endregion
}