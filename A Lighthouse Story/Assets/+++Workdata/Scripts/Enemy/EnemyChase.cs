using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;

    [SerializeField] private Transform playerTransform;

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
            _enemyMovement.rightTarget = playerTransform;

    }
}
