using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyMovement.enabled = true;
        }
    }
}
