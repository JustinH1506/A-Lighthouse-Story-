using UnityEngine;


public class PlayerInputManager : MonoBehaviour
{
    #region Scripts
    
    private PlayerControllerMap _playerControllerMap;

    private PlayerJumping _playerJumping;

    private PlayerMovement _playerMovement;

    private PlayerObjectMove _playerObjectMove;

    private PlayerClimbing _playerClimbing;

    #endregion
    
    #region Methods
    
    private void Awake()
    {
        _playerControllerMap = new PlayerControllerMap();

        _playerJumping = GetComponent<PlayerJumping>();
        
        _playerMovement = GetComponent<PlayerMovement>();
        
        _playerObjectMove = GetComponent<PlayerObjectMove>();

        _playerClimbing = GetComponent<PlayerClimbing>();
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
        
        _playerControllerMap.Player.MoveObject.started += _playerObjectMove.ConnectObject;
        _playerControllerMap.Player.MoveObject.canceled += _playerObjectMove.DisconnectObject;
    }
    
    private void OnDisable()
    {
        _playerControllerMap.Disable();
        
        _playerControllerMap.Player.Jump.performed -= _playerJumping.Jump;
        _playerControllerMap.Player.Jump.canceled -= _playerJumping.Jump;
        
        _playerControllerMap.Player.Move.performed -= _playerMovement.Move;
        _playerControllerMap.Player.Move.canceled -= _playerMovement.Move;
        
        //_playerControllerMap.Player.Climb.performed -= _playerClimbing.Climb;
        //_playerControllerMap.Player.Climb.canceled -= _playerClimbing.Climb;
        
        _playerControllerMap.Player.MoveObject.started -= _playerObjectMove.ConnectObject;
        _playerControllerMap.Player.MoveObject.canceled -= _playerObjectMove.DisconnectObject;
    }

    /*private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Climbable"))
        {
            _playerMovement.enabled = false;

            _playerClimbing.enabled = true;

            _playerClimbing.rb.useGravity = false;
        }
    }*/

    /*private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Climbable"))
        {
            _playerMovement.enabled = true;

            //_playerClimbing.enabled = false;
            
            _playerClimbing.rb.useGravity = true;
        }
    }*/

    #endregion
}