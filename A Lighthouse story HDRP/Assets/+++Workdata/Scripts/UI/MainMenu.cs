using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject oceanPlane;
    
    private AutoFlip autoFlipScript;

    private void Awake()
    {
        autoFlipScript = FindObjectOfType<AutoFlip>();
    }

    /// <summary>
    /// starts new Game and loads the level
    /// </summary>
    public void StartNewGame()
    {
        GameStateManager.instance.StartNewGame();
    }

    /// <summary>
    /// flips the book one time to the right
    /// </summary>
    public void FlipPage()
    {
        autoFlipScript.FlipRightPage(0);
    }

    /// <summary>
    /// disables the ocean Gameobject
    /// </summary>
    public void DisableOcean()
    {
        oceanPlane.SetActive(false);
    }

    /// <summary>
    /// loads Credits scene
    /// </summary>
    public void StartCredits()
    {
        LoadSceneManager.instance.SwitchScene("Credits");
    }
}
