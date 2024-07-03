using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDemo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameUI.Instance.DeactivateSearchingEye();
            
            InGameUI.Instance.ShowEndofDemo();
        }
    }
}
