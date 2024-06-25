using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{
    #region Scripts
    
    private PlayerControllerMap _playerControllerMap;

    private PlayerJumping _playerJumping;

    private PlayerMovement _playerMovement;

    private PlayerObjectMove _playerObjectMove;

    #endregion
    
    #region Methods
    
    /// <summary>
    /// We create a new PlayerControllerMap and get the PlayerJumping, PlayerMovement and PlayerObjectMove. 
    /// </summary>
    private void Awake()
    {
        _playerControllerMap = new PlayerControllerMap();

        _playerJumping = GetComponent<PlayerJumping>();
        
        _playerMovement = GetComponent<PlayerMovement>();
        
        _playerObjectMove = GetComponent<PlayerObjectMove>();
    }

    
    /// <summary>
    /// Enable the PlayerControllerMap.
    /// Subscribe methods to certain buttons. 
    /// </summary>
    private void OnEnable()
    {
        _playerControllerMap.Enable();

        _playerControllerMap.Player.Jump.performed += _playerJumping.Jump;
        _playerControllerMap.Player.Jump.canceled += _playerJumping.Jump;
        
        _playerControllerMap.Player.Move.performed += _playerMovement.Move;
        _playerControllerMap.Player.Move.canceled += _playerMovement.Move;
        
        _playerControllerMap.Player.Sneak.performed += _playerMovement.Sneak;
        
        _playerControllerMap.Player.Sprint.performed += _playerMovement.Sprint;
        
        _playerControllerMap.Player.MoveObject.started += _playerObjectMove.ConnectObject;
        _playerControllerMap.Player.MoveObject.canceled += _playerObjectMove.DisconnectObject;
    }
    
    /// <summary>
    /// Disable the PlayerControllerMap.
    /// Desubscribe methods to certain buttons. 
    /// </summary>
    private void OnDisable()
    {
        _playerControllerMap.Disable();
        
        _playerControllerMap.Player.Jump.performed -= _playerJumping.Jump;
        _playerControllerMap.Player.Jump.canceled -= _playerJumping.Jump;
        
        _playerControllerMap.Player.Move.performed -= _playerMovement.Move;
        _playerControllerMap.Player.Move.canceled -= _playerMovement.Move;
        
        _playerControllerMap.Player.MoveObject.started -= _playerObjectMove.ConnectObject;
        _playerControllerMap.Player.MoveObject.canceled -= _playerObjectMove.DisconnectObject;
    }

    #endregion
}