using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerJumping _playerJumping;
    
    /// <summary>
    /// Makes player jump possible. 
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerJumping.canJump = true;
        }
    }

    /// <summary>
    /// Makes player jump impossible. 
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerJumping.canJump = false;
        }
    }
}
