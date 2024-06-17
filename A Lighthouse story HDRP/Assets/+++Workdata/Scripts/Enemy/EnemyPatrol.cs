using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Transform rightTarget;
    
    [SerializeField] private Transform leftTarget;
    
    [SerializeField] private Transform playerPosition;
    
    public bool isLeft, isRight;
    
    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (!isLeft && !isRight)
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
        }
    } 
}

