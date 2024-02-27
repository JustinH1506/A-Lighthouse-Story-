using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectMove : PlayerBase
{
    private bool raycastHit;
    
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
        Debug.Log("Set to Child!");
        
        if (raycastHit)
        {
            moveableObject.transform.SetParent(transform);
        }
    }

    public void LoseObject(InputAction.CallbackContext context)
    {
        Debug.Log("Unset");
        
        if(raycastHit)
        {
            moveableObject.transform.SetParent(null);
        }
    }

}
