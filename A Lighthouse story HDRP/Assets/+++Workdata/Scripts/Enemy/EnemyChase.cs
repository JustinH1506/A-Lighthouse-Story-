using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;

    private bool playerIsTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsTarget = true;
        }
    }

    private void Update()
    {
        if (playerIsTarget)
        {
            _enemyMovement.isLeft = false;
            _enemyMovement.isRight = false;
        }    
    }
}
