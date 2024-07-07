using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCage : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    public void StartCredits()
    {
        LoadSceneManager.instance.SwitchScene("Credits", false);
    }
}
