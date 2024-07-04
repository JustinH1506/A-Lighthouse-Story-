using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject skipText;

    private PlayerControllerMap inputActions;

    //in game menu script
    private GameObject inGameUI;

    //bool to check if player can skip the credits
    private bool allowSkip;
    
    #endregion

    #region Unity Methods
    private void Awake()
    {
        inputActions = new PlayerControllerMap();
        if (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
        {
            inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").gameObject;
            inGameUI.SetActive(false);
        }

        skipText.SetActive(false);
        StartCoroutine(WaitForSkip());
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.UI.SkipCredits.performed += SkipCredits;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.UI.SkipCredits.performed -= SkipCredits;
    }
    
    #endregion

    #region Input Methods
    //if the allowSkip is true, loads main menu and set allowSkip back to false
    private void SkipCredits(InputAction.CallbackContext context)
    {
        if (context.performed && allowSkip)
        {
            allowSkip = false;
            GameStateManager.instance.GoToMainMenu();
        }
    }
    
    #endregion

    #region Credit Methods
    //loads the main menu
    public void EndCredits()
    {
        GameStateManager.instance.GoToMainMenu();
    }

    /// <summary>
    /// waits 2 seconds and turn allowSkip to true
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForSkip()
    {
        yield return new WaitForSeconds(2f);
        
        allowSkip = true;
        skipText.SetActive(true);
    }
    
    #endregion
}
