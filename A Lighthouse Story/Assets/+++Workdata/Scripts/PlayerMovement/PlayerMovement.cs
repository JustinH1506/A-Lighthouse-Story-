using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerBase
{
    #region Variables
    
    [Header("Movement")]
    [Tooltip("Speed to make the Player Move (Has to be negative.)")]
    [Space(5)]
    [SerializeField] private float moveSpeed, rotationSpeed;

    private float inputX, inputZ;

    private bool isSneaking;
    
    private bool isSprinting;

    #endregion
    
    #region Methods

    private void OnEnable()
    {
        _playerControllerMap.Enable();

        _playerControllerMap.Player.Move.performed += Move;
        _playerControllerMap.Player.Move.canceled += Move;
        
        _playerControllerMap.Player.Sneak.performed += Sneak;
        
        _playerControllerMap.Player.Sprint.performed += Sprint;
    }

    private void OnDisable()
    {
        _playerControllerMap.Disable();
        
        _playerControllerMap.Player.Move.performed -= Move;
        _playerControllerMap.Player.Move.canceled -= Move;
    }

    public void FixedUpdate()
    {
        
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 forwardRelativeMovementVector = inputZ * cameraForward;
        Vector3 rightRelativeMovementVector = inputX * cameraRight;

        Vector3 cameraRelativeMovement = forwardRelativeMovementVector + rightRelativeMovementVector;
        cameraRelativeMovement.Normalize();

        transform.Translate(cameraRelativeMovement * moveSpeed * Time.deltaTime, Space.World);
        
        if (cameraRelativeMovement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(cameraRelativeMovement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector3>().x;
        
        inputZ = context.ReadValue<Vector3>().z;
    }

    private void Sneak(InputAction.CallbackContext context)
    {
        if (context.performed && !isSneaking)
        {
            isSneaking = true;

            isSprinting = false;
            
            moveSpeed = 3;
        }
        else
        {
            isSneaking = false;

            moveSpeed = 7;
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && !isSprinting)
        {
            isSprinting = true;

            isSneaking = false;
            
            moveSpeed = 9;
        }
        else
        {
            isSprinting = false;

            moveSpeed = 7;
        }
            
    }
    #endregion
}