using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Transform rightTarget;
    
    [SerializeField] private Transform leftTarget;
    
    public bool isLeft, isRight;

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (transform.position != leftTarget.position && !isLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftTarget.position, moveSpeed);
        }
        else if (transform.position == leftTarget.position) 
        { 
            isLeft = true; 
            isRight = false;
        }
        
        if (transform.position != rightTarget.position && !isRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightTarget.position, moveSpeed);
        }
        else if (transform.position == rightTarget.position) 
        { 
            isLeft = false; 
            isRight = true;
        }
    } 
}

