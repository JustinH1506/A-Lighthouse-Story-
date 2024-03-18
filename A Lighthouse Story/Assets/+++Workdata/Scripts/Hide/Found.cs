using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    [SerializeField] private Transform safePosition;
    
    public void GameOver(Transform target)
    {
        target.position = safePosition.position;
    }
}
