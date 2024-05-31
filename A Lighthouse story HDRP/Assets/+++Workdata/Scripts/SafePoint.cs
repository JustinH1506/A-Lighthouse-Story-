using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        _playerMovement.transform.position = transform.position;

        _playerMovement.positionData.safePoint = transform;
    }
}
