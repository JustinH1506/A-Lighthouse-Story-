using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject ocean;
    
    private Animator mainMenuAnim;

    private bool canPressAnyButton = true;

    private PlayerControllerMap inputActions;

    private AutoFlip autoFlipScript;
    
    #endregion

    #region Unity Methods
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
    
    #endregion

    #region Input Methods
    private void PressAnyButton(InputAction.CallbackContext context)
    {
        if (context.performed && canPressAnyButton && LoadSceneManager.instance.sceneLoaded)
        {
            mainMenuAnim.Play("Fade_In_Main");
            canPressAnyButton = false;
        }
    }
    
    #endregion

    #region Main Menu Methods
    /// <summary>
    /// starts new Game and loads the level
    /// </summary>
    public void StartNewGame()
    {
        GameStateManager.instance.DeletePlayerPrefsPosition();
        LoadSceneManager.instance.SwitchScene("Story", false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// flips the book one time to the right
    /// </summary>
    public void FlipPage()
    {
        autoFlipScript.FlipRightPage(0);
    }

    /// <summary>
    /// adjust ocean position for animation
    /// </summary>
    public void AdjustOcean()
    {
        ocean.transform.position = new Vector3(0, -0.65f, 0);
    }

    /// <summary>
    /// disables the ocean Gameobject
    /// </summary>
    public void DisableOcean()
    {
        ocean.SetActive(false);
    }

    /// <summary>
    /// loads Credits scene
    /// </summary>
    public void StartCredits()
    {
        LoadSceneManager.instance.SwitchScene("Credits", false);
    }
    
    #endregion
}
