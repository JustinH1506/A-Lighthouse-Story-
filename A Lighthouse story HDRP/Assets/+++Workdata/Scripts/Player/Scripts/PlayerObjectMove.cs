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
    
    public bool isMoving;

    [Tooltip("Offsets the Raycast.")]
    [SerializeField] private float offset;

    [SerializeField] private LayerMask targetLayer;
    #endregion

    #region Components

    private SpringJoint _springJoint;

    private FixedJoint _fixedJoint;

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
                Debug.Log("Chest got it");
                moveableObject = hit.collider.gameObject;
            }
        }
        else
        {
             moveableObject = null;

            //moveableObjectRb = null;
        }

        if(isMoving && moveableObject != null && moveableObjectRb != null)
        {
            /*Vector3 forceDirection = moveableObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            
            moveableObjectRb.AddForceAtPosition(forceDirection * (pushPower * playerMovement.inputX), transform.position, ForceMode.Impulse);
            
            Debug.Log(moveableObjectRb.velocity);*/
            
            transform.LookAt(new Vector3(moveableObject.transform.position.x, transform.position.y, moveableObject.transform.position.z));
        }
    }

    /// <summary>
    /// We connect the Rigidbody to our SpringJoint to have it moving. 
    /// </summary>
    /// <param name="context"></param>
    public void ConnectObject(InputAction.CallbackContext context)
    {
        if (moveableObject != null)
        {
            //moveableObjectRb = moveableObject.GetComponent<Rigidbody>();
            
            //_springJoint.connectedBody = moveableObjectRb;

            //moveableObjectRb.mass = 1;
            
            //moveableObject.transform.SetParent(transform);

            _fixedJoint = moveableObject.gameObject.GetComponent<FixedJoint>();

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
        if(moveableObject != null)
        {
            moveableObjectRb = null;

            _fixedJoint.connectedBody = null;
            
            //moveableObject.transform.SetParent(moveableObjectParent);
            
            isMoving = false;
            
            //_springJoint.connectedBody = null;
            
            //moveableObjectRb.mass = 100;
        }
    }
    #endregion
}