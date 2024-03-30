using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [FormerlySerializedAs("movement")] [SerializeField] private float moveSpeed;

    [SerializeField] private Transform rightTarget;
    
    [SerializeField] private Transform leftTarget;

    private bool isLeft = true, isRight;

    private Rigidbody rb;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
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
        
        if(transform.position != leftTarget.position && !isLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftTarget.position, moveSpeed);
        }
        else if (transform.position == leftTarget.position)
        {
            isLeft = true;
            isRight = false;
        }
    }
}
