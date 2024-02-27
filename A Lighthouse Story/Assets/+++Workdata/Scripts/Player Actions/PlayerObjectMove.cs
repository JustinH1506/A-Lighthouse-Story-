using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectMove : PlayerBase
{
    [SerializeField] private PlayerMovement _playerMovement;
    
    private bool raycastHit;

    public bool isMoving;
    
    private GameObject moveableObject;

    [SerializeField] private Transform startPos;

    private void FixedUpdate()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(startPos.position, startPos.forward, out hit, 1))
        {
            Debug.DrawRay(transform.position, startPos.forward, Color.green);
            
            if(hit.collider.CompareTag("Chest"))
            {
                raycastHit = true;
                
                moveableObject = hit.collider.gameObject;
            }
            else
            {
                moveableObject = null;
                
                raycastHit = false;
            }
        }
    }

    public void GetObject(InputAction.CallbackContext context)
    {
        if (raycastHit)
        {
            moveableObject.transform.SetParent(transform);

            isMoving = true;

            _playerMovement.moveSpeed = 3;
        }
    }

    public void LoseObject(InputAction.CallbackContext context)
    {
        if(raycastHit)
        {
            moveableObject.transform.SetParent(null);

            isMoving = false;
            
            _playerMovement.moveSpeed = 7;
        }
    }

}
