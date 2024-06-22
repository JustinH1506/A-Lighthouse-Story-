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
    
    [SerializeField] private bool moveBack;

    public bool foundPlayer;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (foundPlayer)
        {
            _navMeshAgent.SetDestination(playerPosition.position);

            _navMeshAgent.angularSpeed = 120;
            
            _anim.SetFloat("speed", -1f);
            
            return;
        }
        
        _anim.SetTrigger("isWalking");
        
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

        /*if (!isLeft && !isRight)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, moveSpeed);

                    transform.LookAt(playerPosition);

                    return;
                }

                if (isRight && !isLeft)
                {
                    transform.position = Vector3.MoveTowards(transform.position, leftTarget.position, moveSpeed);

                    if (Vector3.Distance(transform.position, leftTarget.position) < 1f)
                    {
                        isLeft = true;
                        isRight = false;
                    }
                }

                if (isLeft && !isRight)
                {
                    transform.position = Vector3.MoveTowards(transform.position, rightTarget.position, moveSpeed);

                    if (Vector3.Distance(transform.position, rightTarget.position) < 1f)
                    {
                        isLeft = false;
                        isRight = true;
                    }
                }*/
    } 
}

