using System;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Light lampLight;
    
    #region Method

    /// <summary>
    /// Changes position of player and safes it. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        lampLight.enabled = true;
        
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.position.z);
    }
    
    #endregion
}
