using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectMove : PlayerBase
{
    #region Scripts
    
    [SerializeField] private PlayerMovement _playerMovement;
    
    #endregion
    
    #region Variables
    
    public bool isMoving;
    
    private bool raycastHit;

    [Tooltip("Maximum distance for the Raycast")]
    [Range(0, 10)]
    [SerializeField] private float raycastDistance;

    #endregion
    
    #region Declared Objects
    
    private GameObject moveableObject;

    [SerializeField] private Transform startPos;
    
    #endregion

    #region Methods
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
            moveableObject.transform.SetParent(transform);

            isMoving = true;

            _playerMovement.maxSpeed = 0.5f;
        }
    }

    public void LoseObject(InputAction.CallbackContext context)
    {
        if(raycastHit)
        {
            moveableObject.transform.SetParent(null);

            isMoving = false;
            
            _playerMovement.maxSpeed = 2;
        }
    }
    #endregion
}