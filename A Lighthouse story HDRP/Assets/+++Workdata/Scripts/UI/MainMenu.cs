using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject oceanPlane;
    private Animator mainMenuAnim;

    private bool canPressAnyButton = true;

    private PlayerControllerMap inputActions;
    
    private AutoFlip autoFlipScript;

    private void Awake()
    {
        mainMenuAnim = GetComponent<Animator>();
        inputActions = new PlayerControllerMap();
        autoFlipScript = FindObjectOfType<AutoFlip>();
        canPressAnyButton = true;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.UI.PressAnyButton.performed += PressAnyButton;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.UI.PressAnyButton.performed -= PressAnyButton;
    }

    private void PressAnyButton(InputAction.CallbackContext context)
    {
        if (context.performed && canPressAnyButton)
        {
            mainMenuAnim.Play("Fade_In_Main");
            canPressAnyButton = false;
        }
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
