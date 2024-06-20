using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    #region classes
    
    [SerializeField] private EnemyChase enemyChase;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private CinemachineFreeLook crabShot;
    
    #endregion

    #region Variables

    [SerializeField] private float crabShotWaitTime;
    
    [SerializeField] private float moveAgainWaitTime;

    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitTimeDuringCrabShot());
        }
    }

    IEnumerator WaitTimeDuringCrabShot()
    {
        _playerMovement.DisableMovement();
        
        crabShot.enabled = true;

        yield return new WaitForSeconds(crabShotWaitTime);

        crabShot.enabled = false;
        
        enemyChase.enabled = true;
        
        //yield return new WaitForSeconds(moveAgainWaitTime);

        _playerMovement.isDisabled = false;
    }
}
