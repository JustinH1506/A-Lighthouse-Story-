using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject skipText;

    private PlayerControllerMap inputActions;

    private GameObject inGameUI;

    private bool allowSkip;

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

    private void SkipCredits(InputAction.CallbackContext context)
    {
        if (context.performed && allowSkip)
        {
            allowSkip = false;
            GameStateManager.instance.GoToMainMenu();
        }
    }

    public void EndCredits()
    {
        GameStateManager.instance.GoToMainMenu();
    }

    private IEnumerator WaitForSkip()
    {
        yield return new WaitForSeconds(2f);
        
        allowSkip = true;
        skipText.SetActive(true);
    }
}
