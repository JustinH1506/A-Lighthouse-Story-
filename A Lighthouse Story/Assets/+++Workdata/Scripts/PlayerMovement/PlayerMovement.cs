using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region Scripts
    
    private PlayerControllerMap _playerControllerMap;
    
    #endregion
    
    #region Variables

    private Rigidbody rb;
    
    [Header("Movement")]
    [Tooltip("Speed to make the Player Move (Has to be negative.)")]
    [Space(5)]
    [SerializeField] private float moveSpeed;

    private float inputX, inputZ;

    #endregion
    
    #region Methods
    private void Awake()
    {
        _playerControllerMap = new PlayerControllerMap();

        rb = GetComponent<Rigidbody>();
    }

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
        //rb.velocity = new Vector3(inputX * moveSpeed, rb.velocity.y, inputZ * moveSpeed);

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = Vector3.forward.normalized;
        cameraRight = Vector3.right.normalized;

        Vector3 forwardRelativeMovementVector = inputZ * cameraForward;
        Vector3 rightRelativeMovementVector = inputX * cameraRight;

        Vector3 cameraRelativeMovement = forwardRelativeMovementVector + rightRelativeMovementVector;

        transform.Translate(cameraRelativeMovement, Space.World);
    }

    private void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector3>().x;
        
        inputZ = context.ReadValue<Vector3>().z;
    }
    #endregion
}
