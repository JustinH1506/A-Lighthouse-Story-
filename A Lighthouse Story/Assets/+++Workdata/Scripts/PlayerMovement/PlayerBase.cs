using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region Scripts
    
    public PlayerControllerMap _playerControllerMap;
    
    #endregion
    
    #region Variables

    public Rigidbody rb;
    
    #endregion

    private void Awake()
    {
        _playerControllerMap = new PlayerControllerMap();

        rb = GetComponent<Rigidbody>();
    }
}
