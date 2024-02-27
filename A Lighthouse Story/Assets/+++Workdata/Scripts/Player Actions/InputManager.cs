using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControllerMap _playerControllerMap;

    [SerializeField] private PlayerJumping _playerJumping;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private PlayerObjectMove _playerObjectMove;

    private void Awake()
    {
        _playerControllerMap = new PlayerControllerMap();
    }

    private void OnEnable()
    {
        _playerControllerMap.Enable();

        _playerControllerMap.Player.Jump.performed += _playerJumping.Jump;
        _playerControllerMap.Player.Jump.canceled += _playerJumping.Jump;
        
        _playerControllerMap.Player.Move.performed += _playerMovement.Move;
        _playerControllerMap.Player.Move.canceled += _playerMovement.Move;
        
        _playerControllerMap.Player.Sneak.performed += _playerMovement.Sneak;
        
        _playerControllerMap.Player.Sprint.performed += _playerMovement.Sprint;
        
        _playerControllerMap.Player.MoveObject.started += _playerObjectMove.GetObject;
        _playerControllerMap.Player.MoveObject.canceled += _playerObjectMove.LoseObject;
    }
    
    private void OnDisable()
    {
        _playerControllerMap.Disable();
        
        _playerControllerMap.Player.Jump.performed -= _playerJumping.Jump;
        _playerControllerMap.Player.Jump.canceled -= _playerJumping.Jump;
        
        _playerControllerMap.Player.Move.performed -= _playerMovement.Move;
        _playerControllerMap.Player.Move.canceled -= _playerMovement.Move;
        
        _playerControllerMap.Player.MoveObject.started -= _playerObjectMove.GetObject;
        _playerControllerMap.Player.MoveObject.canceled -= _playerObjectMove.LoseObject;
    }
}
