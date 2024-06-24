using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerObjectMove : PlayerBase
{
    #region Variables
    
    public bool isMoving;

    [Tooltip("Maximum distance for the Raycast")]
    [Range(0, 10)]
    [SerializeField] private float raycastDistance;

    [SerializeField] private LayerMask targetLayer;

    #endregion

    #region Components

    private SpringJoint _springJoint;

    #endregion
    
    #region Objects
    
    private GameObject moveableObject;

    public Rigidbody moveableObjectRb;

    [SerializeField] private Transform startPos;
    
    #endregion

    #region Methods

    /// <summary>
    /// Get SpringJoint in Children.
    /// </summary>
    private void Awake()
    {
        _springJoint = GetComponentInChildren<SpringJoint>();
    }

    /// <summary>
    /// Shoots a Raycast to find moveable objects and sets one if found to moveableObject. 
    /// </summary>
    private void FixedUpdate()
    {
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, startPos.forward, Color.green);
        
        if (Physics.Raycast(startPos.position, startPos.forward, out hit, raycastDistance,targetLayer))
        {
            if(hit.collider.CompareTag("Chest"))
            {
                Debug.Log("Chest got it");
                moveableObject = hit.collider.gameObject;
            }
        }
        else
        {
            moveableObject = null;
        }

        if(isMoving && moveableObject != null)
        {
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

            _springJoint.connectedBody = moveableObjectRb;

            moveableObjectRb.mass = 1;
            
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
            isMoving = false;
            
            _springJoint.connectedBody = null;
            
            moveableObjectRb.mass = 100;
        }
    }
    #endregion
}