using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
     
    [SerializeField] private Transform rightTarget;
    
    [SerializeField] private Transform leftTarget;

    [SerializeField] private Transform playerTransform;

    public bool isLeft, isRight;
    
    private void FixedUpdate()
    {
        if(isLeft || isRight)
        {
            if (transform.position != rightTarget.position && !isRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, rightTarget.position, moveSpeed);
            }
            else if (transform.position == rightTarget.position)
            {
                isRight = true;
                isLeft = false;
            }

            if (transform.position != leftTarget.position && !isLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, leftTarget.position, moveSpeed);
            }
            else if (transform.position == leftTarget.position)
            {
                isLeft = true;
                isRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,playerTransform.position, moveSpeed);
        }
    }
}
