using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClimbing : PlayerBase
{
    #region Scripts

    private PlayerMovement _playerMovement;
    
    #endregion
    
    #region Variables
    
    [SerializeField] private float maxSpeed;

    [SerializeField] private float acceleration;

    [SerializeField] private float deceleration;

    private float inputY;
    
    #endregion

    #region Methods
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void FixedUpdate()
    {
        if(inputY != 0)
        {
            Debug.Log(inputY);
            
            rb.AddForce(new Vector3(rb.velocity.x, inputY * acceleration, rb.velocity.z), ForceMode.Force);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            rb.AddForce(new Vector3(rb.velocity.x, inputY * deceleration, rb.velocity.z), ForceMode.Force);
        }
    }

    public void Climb(InputAction.CallbackContext context)
    {
        inputY = context.ReadValue<Vector3>().y;
    }
    #endregion
}
