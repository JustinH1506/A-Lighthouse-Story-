using System.Collections;
using Cinemachine;
using UnityEngine;

public class EnemyPatrolStart : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook hideCrabShot;

    [SerializeField] private EnemyPatrol _enemyPatrol;

    [SerializeField] private PlayerMovement _playerMovement;

    
    /// <summary>
    /// Starts WaitTimeDuringHideCrabShot.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitTimeDuringHideCrabShot());
        }
    }
    
    /// <summary>
    /// Disables Players movement and changes camera. Waits for a while and brings the camera back. 
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitTimeDuringHideCrabShot()
    {
        _playerMovement.DisableMovement();
        
        hideCrabShot.enabled = true;

        yield return new WaitForSeconds(3f);
        
        InGameUI.Instance.ActivateSearchingEye();
        
        InGameUI.Instance.EyeSearchingState();
        
        MusicManager.Instance.PlayMusic(MusicManager.Instance.crabSearchMusic, 1f);

        hideCrabShot.enabled = false;
        
        _enemyPatrol.enabled = true;

        _playerMovement.isDisabled = false;
        
        gameObject.SetActive(false);
    }
}
