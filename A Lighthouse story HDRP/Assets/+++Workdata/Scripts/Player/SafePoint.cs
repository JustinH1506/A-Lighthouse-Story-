using UnityEngine;

public class SafePoint : MonoBehaviour
{
    #region Classes

    [SerializeField] private PlayerMovement _playerMovement;
    
    #endregion

    #region Method
    
    /// <summary>
    /// Changes position of player and safes it. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        _playerMovement.transform.position = transform.position;

        _playerMovement.positionData.safePoint = transform;
    }
    
    #endregion
}
