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

    #endregion
    
    #region Methods

    private void OnEnable()
    {
        _playerControllerMap.Enable();

        _playerControllerMap.Player.Move.performed += Move;
        _playerControllerMap.Player.Move.canceled += Move;
    }

    private void OnDisable()
    {
        _playerControllerMap.Disable();
        
        _playerControllerMap.Player.Move.performed -= Move;
        _playerControllerMap.Player.Move.canceled -= Move;
    }

    public void FixedUpdate()
    {
        /*rb.velocity = new Vector3(inputX * moveSpeed, rb.velocity.y, inputZ * moveSpeed);

        if (rb.velocity != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rb.velocity, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }*/

        /*Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;*/
        
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
    #endregion
}
