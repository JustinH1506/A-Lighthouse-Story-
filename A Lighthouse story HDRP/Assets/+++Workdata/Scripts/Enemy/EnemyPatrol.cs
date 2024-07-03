using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform rightTarget;
    
    [SerializeField] private Transform leftTarget;
    
    [SerializeField] private Transform playerPosition;
    
    private NavMeshAgent _navMeshAgent;

    private Animator _anim;
    
    [SerializeField] private bool moveBack;

    public bool foundPlayer;

    /// <summary>
    /// Get NavMesh and Animator. 
    /// </summary>
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Call Patrol method.
    /// </summary>
    private void FixedUpdate()
    {
        Patrol();
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
}

