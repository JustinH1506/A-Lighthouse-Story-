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

    private void Awake()
    {
        _springJoint = GetComponentInChildren<SpringJoint>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        
        Debug.DrawRay(transform.position, startPos.forward, Color.green);
        
        if (Physics.Raycast(startPos.position, startPos.forward, out hit, raycastDistance,1))
        {
            if(hit.collider.CompareTag("Chest"))
            {
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

    public void ConnectObject(InputAction.CallbackContext context)
    {
        if (moveableObject != null)
        {
            moveableObjectRb = moveableObject.GetComponent<Rigidbody>();

            //_springJoint.connectedBody = moveableObjectRb;

            //moveableObjectRb.mass = 1;
            
            isMoving = true;
        }
    }

    public void DisconnectObject(InputAction.CallbackContext context)
    {
        if(moveableObject != null)
        {
            isMoving = false;
            
            //_springJoint.connectedBody = null;
            
            //moveableObjectRb.mass = 100;
        }
    }
    #endregion
}