using UnityEngine;

public class SafePoint : MonoBehaviour
{
    #region Classes

    [SerializeField] private PlayerMovement _playerMovement;
    
    #endregion

    #region Method
    private void OnTriggerEnter(Collider other)
    {
        _playerMovement.transform.position = transform.position;

        _playerMovement.positionData.safePoint = transform;
    }
    
    #endregion
}
