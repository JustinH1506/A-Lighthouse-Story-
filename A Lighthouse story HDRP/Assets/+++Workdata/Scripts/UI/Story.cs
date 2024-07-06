using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Story : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject skipText;
    
    private PlayerControllerMap inputActions;

    //in game menu script
    private GameObject inGameUI;

    //bool to check if player can skip the story
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

    #region Story Methods
    //if the allowSkip is true, loads the level and set allowSkip back to false
    private void SkipCredits(InputAction.CallbackContext context)
    {
        if (context.performed && allowSkip)
        {
            allowSkip = false;
            StartLevel();
        }
    }

    public void StartLevel()
    {
        GameStateManager.instance.StartNewGame();
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
