using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform rightTarget;
    
    [SerializeField] private Transform leftTarget;
    
    [SerializeField] private Transform playerPosition;
    
    private NavMeshAgent _navMeshAgent;

    private Animator _anim;

    [SerializeField] private Death _death;
    
    [SerializeField] private bool moveBack;

    public bool foundPlayer;

    /// <summary>
    /// Get NavMesh and Animator. 
    /// </summary>
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();

        _anim.enabled = true;
    }

    /// <summary>
    /// Call Patrol method.
    /// </summary>
    private void FixedUpdate()
    {
        Patrol();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InGameUI.Instance.DeactivateSearchingEye();
            
            _death.StartDeathAnimation();
        }
    }

    /// <summary>
    /// Depends on where the enemy is walks to the left or right.
    /// If the enemy found a Player it will abandon the patrol and walk to the player. 
    /// </summary>
    private void Patrol()
    {
        if (foundPlayer)
        {
            _navMeshAgent.SetDestination(playerPosition.position);

            _navMeshAgent.angularSpeed = 120;
            
            MusicManager.Instance.PlayMusic(MusicManager.Instance.crabChaseMusic, 0.5f);
            
            _anim.SetFloat("speed", -1f);
            
            return;
        }
        
        if (moveBack)
        {
            _navMeshAgent.SetDestination(leftTarget.position);
            
            _anim.SetFloat("speed", 1f);
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                moveBack = false;
                _navMeshAgent.SetDestination(rightTarget.position);
                
                _anim.SetFloat("speed", -1f);
            }
        }
        else
        {
            _navMeshAgent.SetDestination(rightTarget.position);
            
            _anim.SetFloat("speed", -1f);
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    moveBack = true;
                    
                    _anim.SetFloat("speed", -1f);
                }
            }
        }
    }

    public void DeactivateCrabEyeUI()
    {
        InGameUI.Instance.DeactivateSearchingEye();
    }

    public void ActivateCrabEyeUI()
    {
        InGameUI.Instance.ActivateSearchingEye();
    }

    public void ActivateCabinMusic()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Instance.cabinMusic, 0.5f);
    }
}

