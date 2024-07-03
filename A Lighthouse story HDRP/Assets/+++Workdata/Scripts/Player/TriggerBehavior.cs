using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehavior : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerExitEvent?.Invoke();
        }
    }
}
