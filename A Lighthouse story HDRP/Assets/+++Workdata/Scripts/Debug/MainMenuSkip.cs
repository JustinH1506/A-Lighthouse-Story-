using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSkip : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey("8"))
        {
            GameStateManager.instance.StartNewGame();
        }
    }
}
