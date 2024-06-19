using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class EnemyChase : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform player;

    private Rigidbody rb;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        Vector3 direction = (targetPos - transform.position).normalized;
            
        
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
        
        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    
    #endregion
}
