using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrolStart : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook hideCrabShot;

    [SerializeField] private EnemyPatrol _enemyPatrol;

    [SerializeField] private PlayerMovement _playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitTimeDuringHideCrabShot());
        }
    }
    
    IEnumerator WaitTimeDuringHideCrabShot()
    {
        _playerMovement.DisableMovement();
        
        hideCrabShot.enabled = true;

        yield return new WaitForSeconds(2f);

        hideCrabShot.enabled = false;
        
        _enemyPatrol.enabled = true;

        _playerMovement.isDisabled = false;
        
        gameObject.SetActive(false);
    }
}
