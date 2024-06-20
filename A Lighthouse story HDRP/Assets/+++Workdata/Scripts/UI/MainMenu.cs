using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private AutoFlip autoFlipScript;

    private void Awake()
    {
        autoFlipScript = FindObjectOfType<AutoFlip>();
    }

    public void StartNewGame()
    {
        GameStateManager.instance.StartNewGame();
    }

    public void FlipPage()
    {
        autoFlipScript.FlipRightPage(0);
    }
}
