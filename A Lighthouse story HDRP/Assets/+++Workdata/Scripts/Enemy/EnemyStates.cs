using System.Collections;
using Cinemachine;
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
    
    /// <summary>
    /// If the player walks unti the collider Start Coroutine WaitTImeDuringShotCrab.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitTimeDuringCrabShot());
        }
    }

    
    /// <summary>
    /// Call Player movements Disable Movement method.
    /// Enable crabShot.
    /// wait for the crabShotWaitTime value.
    /// crabShot disabled.
    /// enemy chase enabled.
    /// playerMovement siDisabled bool is false.
    /// gameObject gets Set InActive. 
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitTimeDuringCrabShot()
    {
        _playerMovement.DisableMovement();
        
        crabShot.enabled = true;

        MusicManager.Instance.PlayInGameSFX(MusicManager.Instance.crabScreech);
        
        yield return new WaitForSeconds(crabShotWaitTime);
        
        MusicManager.Instance.PlayInGameSFX(MusicManager.Instance.crabSteps);
        
        MusicManager.Instance.PlayMusic(MusicManager.Instance.crabChaseMusic, 1f);

        crabShot.enabled = false;
        
        enemyChase.enabled = true;

        _playerMovement.isDisabled = false;
        
        gameObject.SetActive(false);
    }
}
