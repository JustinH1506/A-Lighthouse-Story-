using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

[RequireComponent(typeof(Rigidbody))]
public class PlayerObjectMove : PlayerBase
{
    #region Variables
    
    public bool isMoving;
    
    private bool raycastHit;

    [Tooltip("Maximum distance for the Raycast")]
    [Range(0, 10)]
    [SerializeField] private float raycastDistance;

    #endregion

    #region Components

    private SpringJoint _springJoint;

    #endregion
    
    #region Objects
    
    private GameObject moveableObject;

    private Rigidbody moveableObjectRb;

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
                raycastHit = true;
                
                moveableObject = hit.collider.gameObject;
            }
        }
        else
        {
            moveableObject = null;
                
            raycastHit = false;
        }
    }

    public void GetObject(InputAction.CallbackContext context)
    {
        if (raycastHit)
        {
            moveableObjectRb = moveableObject.GetComponent<Rigidbody>();

            _springJoint.connectedBody = moveableObjectRb;
            
            isMoving = true;
        }
    }

    public void LoseObject(InputAction.CallbackContext context)
    {
        if(raycastHit)
        {
            _springJoint.connectedBody = null;
            
            isMoving = false;
        }
    }
    #endregion
}